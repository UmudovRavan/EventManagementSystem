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
        public int LocationId { get; internal set; }
        public int EventTypeId { get; internal set; }
        public int OrganizerId { get; internal set; }

        public string Status { get; set; }
        public string StatusColor { get; set; }
        public double Rating { get; set; }

    }
}
