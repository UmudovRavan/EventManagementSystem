using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Notification;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class NotificationService : GenericService<NotificationVM, Notification>, INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public NotificationService(
            INotificationRepository notificationRepository,
            IInvitationRepository invitationRepository,
            IPersonRepository personRepository,
            IEventRepository eventRepository,
            IMapper mapper)
            : base(notificationRepository, mapper)
        {
            _notificationRepository = notificationRepository;
            _invitationRepository = invitationRepository;
            _personRepository = personRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<NotificationVM> CreateAndSendNotificationAsync(NotificationCreateVM model)
        {
            var notification = _mapper.Map<Notification>(model);
            notification.SentAt = DateTime.Now;
            var result = await _notificationRepository.AddAsync(notification);
            await _notificationRepository.SaveChangesAsync();

            // Xəta buradaydı. NotificationCreateVM daxilində EventId 'int' olduğu güman edilir.
            // Ona görə də HasValue və Value istifadə edilmir.
        
            return _mapper.Map<NotificationVM>(result);
        }

       
        public async Task<IEnumerable<NotificationVM>> GetAllNotificationsWithDetailsAsync()
        {
            var notifications = await _notificationRepository.GetNotificationsWithEventAsync();
            return _mapper.Map<IEnumerable<NotificationVM>>(notifications);
        }

        public Task SendEventNotificationsAsync(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
