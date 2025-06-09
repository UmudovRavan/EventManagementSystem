using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.ViewModels.Notification;

namespace CodeAcademyEventManagementSystem.Service.Interfaces
{
    public interface INotificationService : IGenericService<NotificationVM, Notification>
    {
        Task<NotificationVM> CreateAndSendNotificationAsync(NotificationCreateVM model);
        Task SendEventNotificationsAsync(int eventId); // Bu metod bildirimləri yaratmaqla məşğul olacaq, göndərməklə yox.
        Task<IEnumerable<NotificationVM>> GetAllNotificationsWithDetailsAsync();
    }
}
