using CodeAcademyEventManagementSystem.ViewModels.Event;

namespace CodeAcademyEventManagementSystem.Areas.Admin.Models
{
    public class DashboardVM
    {
        public List<LocationStatVM> LocationStats { get; set; }
        public List<EventVM> CurrentEvents { get; set; }
    }
}
