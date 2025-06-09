namespace CodeAcademyEventManagementSystem.ViewModels.Event
{
    public class EventVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string LocationName { get; set; }

        public string EventTypeName { get; set; }

        public string OrganizerName { get; set; }
    }
}
