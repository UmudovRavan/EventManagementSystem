namespace CodeAcademyEventManagementSystem.Entities
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
