using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.ViewModels.Feedback;

namespace CodeAcademyEventManagementSystem.Service.Interfaces
{
    public interface IFeedbackService : IGenericService<FeedbackVM, Feedback>
    {
        Task<FeedbackVM> AddFeedbackAsync(FeedbackCreateVM model);
        Task<IEnumerable<FeedbackVM>> GetFeedbackForEventAsync(int eventId);
        Task<double> GetAverageRatingForEventAsync(int eventId);
        Task<IEnumerable<FeedbackVM>> GetAllFeedbacksWithDetailsAsync();
    }
}
