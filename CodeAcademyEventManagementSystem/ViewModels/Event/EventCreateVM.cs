using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.ViewModels.Event
{
    public class EventCreateVM
    {
        [Required(ErrorMessage = "Başlıq sahəsi məcburidir.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Başlıq 3 ilə 100 simvol arasında olmalıdır.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıqlama sahəsi məcburidir.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Açıqlama 10 ilə 500 simvol arasında olmalıdır.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Tarix sahəsi məcburidir.")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Məkan seçimi məcburidir.")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Tədbir növü seçimi məcburidir.")]
        public int EventTypeId { get; set; }

        [Required(ErrorMessage = "Təşkilatçı seçimi məcburidir.")]
        public int OrganizerId { get; set; }

        public IEnumerable<SelectListItem> Locations { get; set; }

        public IEnumerable<SelectListItem> Organizers { get; set; }

        public IEnumerable<SelectListItem> EventTypes { get; set; }
    }
}
