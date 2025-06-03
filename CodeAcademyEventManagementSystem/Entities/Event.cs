namespace CodeAcademyEventManagementSystem.Entities
{
    public class Event : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
