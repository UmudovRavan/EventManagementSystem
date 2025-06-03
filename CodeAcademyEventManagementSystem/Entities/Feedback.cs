namespace CodeAcademyEventManagementSystem.Entities
{
    public class Feedback : BaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
