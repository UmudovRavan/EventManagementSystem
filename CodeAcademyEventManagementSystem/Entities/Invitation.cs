namespace CodeAcademyEventManagementSystem.Entities
{
    public class Invitation : BaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string Status { get; set; }
        public DateTime SentAt { get; set; }
        public Participation Participation { get; set; }
    }
}
