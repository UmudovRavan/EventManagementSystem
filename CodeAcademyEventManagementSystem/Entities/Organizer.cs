namespace CodeAcademyEventManagementSystem.Entities
{
    public class Organizer : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
