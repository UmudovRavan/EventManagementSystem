using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.ViewModels.EventType
{
    public class EventTypeCreateVM
    {
        [Required(ErrorMessage = "Tədbir növü adı məcburidir.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Tədbir növü adı 2 ilə 50 simvol arasında olmalıdır.")]
        public string Name { get; set; }
    }
}
