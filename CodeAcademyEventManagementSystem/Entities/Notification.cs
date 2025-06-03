namespace CodeAcademyEventManagementSystem.Entities
{
    public class Notification : BaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }
}
