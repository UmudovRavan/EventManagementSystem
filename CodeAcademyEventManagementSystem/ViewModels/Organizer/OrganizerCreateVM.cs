using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.ViewModels.Organizer
{
    public class OrganizerCreateVM
    {
        [Required(ErrorMessage = "Tam ad sahəsi məcburidir.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tam ad 3 ilə 100 simvol arasında olmalıdır.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "E-poçt sahəsi məcburidir.")]
        [EmailAddress(ErrorMessage = "Keçərli bir e-poçt ünvanı daxil edin.")]
        [StringLength(100, ErrorMessage = "E-poçt 100 simvoldan uzun ola bilməz.")]
        public string Email { get; set; }
    }
}
