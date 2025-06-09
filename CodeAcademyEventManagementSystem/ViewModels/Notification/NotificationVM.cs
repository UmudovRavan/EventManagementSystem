namespace CodeAcademyEventManagementSystem.ViewModels.Notification
{
    public class NotificationVM
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public string EventTitle { get; set; }
    }
}
