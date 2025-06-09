using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Feedback;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class FeedbackService : GenericService<FeedbackVM, Feedback>, IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;

        public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper)
            : base(feedbackRepository, mapper)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        public async Task<FeedbackVM> AddFeedbackAsync(FeedbackCreateVM model)
        {
            var feedback = _mapper.Map<Feedback>(model);
            feedback.SubmittedAt = DateTime.Now;
            var result = await _feedbackRepository.AddAsync(feedback);
            await _feedbackRepository.SaveChangesAsync();
            return _mapper.Map<FeedbackVM>(result);
        }

        public async Task<IEnumerable<FeedbackVM>> GetFeedbackForEventAsync(int eventId)
        {
            var allFeedbacksWithDetails = await _feedbackRepository.GetFeedbacksWithDetailsAsync();
            var filteredFeedbacks = allFeedbacksWithDetails.Where(f => f.EventId == eventId);
            return _mapper.Map<IEnumerable<FeedbackVM>>(filteredFeedbacks);
        }

        public async Task<double> GetAverageRatingForEventAsync(int eventId)
        {
            var allFeedbacksWithDetails = await _feedbackRepository.GetFeedbacksWithDetailsAsync();
            var eventFeedbacks = allFeedbacksWithDetails.Where(f => f.EventId == eventId);

            if (!eventFeedbacks.Any())
            {
                return 0.0;
            }
            return eventFeedbacks.Average(f => f.Rating);
        }

        public async Task<IEnumerable<FeedbackVM>> GetAllFeedbacksWithDetailsAsync()
        {
            var feedbacks = await _feedbackRepository.GetFeedbacksWithDetailsAsync();
            return _mapper.Map<IEnumerable<FeedbackVM>>(feedbacks);
        }
    }
}
