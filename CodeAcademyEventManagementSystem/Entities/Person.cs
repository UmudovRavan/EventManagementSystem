namespace CodeAcademyEventManagementSystem.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }

}
