namespace CodeAcademyEventManagementSystem.ViewModels.Invitation
{
    public class InvitationCreateVM
    {
        public int EventId { get; set; }
        public int PersonId { get; set; }
        public string Status { get; set; }
        public DateTime SentAt { get; set; }
    }
}
