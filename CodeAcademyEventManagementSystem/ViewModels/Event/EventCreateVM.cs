using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.ViewModels.Event
{
    public class EventCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public int EventTypeId { get; set; }

        [Required]
        public int OrganizerId { get; set; }

        public IEnumerable<SelectListItem> Locations { get; set; }

        public IEnumerable<SelectListItem> EventTypes { get; set; }

        public IEnumerable<SelectListItem> Organizers { get; set; }
    }
}
