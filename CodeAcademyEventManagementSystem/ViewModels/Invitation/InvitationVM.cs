namespace CodeAcademyEventManagementSystem.ViewModels.Invitation
{
    public class InvitationVM
    {
        public int Id { get; set; }
        public string EventTitle { get; set; }
        public string PersonName { get; set; }
        public string Status { get; set; }
        public DateTime SentAt { get; set; }
    }
}
