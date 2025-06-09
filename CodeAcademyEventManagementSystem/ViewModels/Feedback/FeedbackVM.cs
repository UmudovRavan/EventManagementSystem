namespace CodeAcademyEventManagementSystem.ViewModels.Feedback
{
    public class FeedbackVM
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string PersonName { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
