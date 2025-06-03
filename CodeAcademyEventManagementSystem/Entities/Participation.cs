namespace CodeAcademyEventManagementSystem.Entities
{
    public class Participation : BaseEntity
    {
        public int InvitationId { get; set; }
        public Invitation Invitation { get; set; }
        public DateTime CheckInTime { get; set; }
        public string SeatNumber { get; set; }
    }
}
