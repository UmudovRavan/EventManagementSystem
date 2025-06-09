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
            if (model.EventId != 0) // Əgər EventId 0 deyilsə, tədbirlə əlaqəlidir deməkdir.
                                    // Yaxud bu if şərti tamamilə ləğv edilə bilər, əgər hər zaman bir EventId gözlənilirsə.
            {
                await SendEventNotificationsAsync(model.EventId); // model.EventId birbaşa ötürülür
            }
            return _mapper.Map<NotificationVM>(result);
        }

        public async Task SendEventNotificationsAsync(int eventId)
        {
            var allInvitationsWithDetails = await _invitationRepository.GetInvitationsWithEventAndPersonAsync();
            var acceptedInvitations = allInvitationsWithDetails
                                        .Where(i => i.EventId == eventId && i.Status == InvitationStatus.Accepted.ToString())
                                        .ToList();

            if (!acceptedInvitations.Any())
            {
                return;
            }

            var eventDetails = (await _eventRepository.GetAllWithDetailsAsync())
                                .FirstOrDefault(e => e.Id == eventId);

            var notificationMessage = (await _notificationRepository.GetNotificationsWithEventAsync())
                                        .FirstOrDefault(n => n.EventId == eventId)?.Message ?? "Etkinlik hatırlatıcısı.";

            foreach (var invitation in acceptedInvitations)
            {
                // E-poçt göndərmə mantığı bu hissədən çıxarılıb.
            }
        }

        public async Task<IEnumerable<NotificationVM>> GetAllNotificationsWithDetailsAsync()
        {
            var notifications = await _notificationRepository.GetNotificationsWithEventAsync();
            return _mapper.Map<IEnumerable<NotificationVM>>(notifications);
        }
    }
}
