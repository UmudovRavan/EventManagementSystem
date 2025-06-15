using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.ViewModels.Person
{
    public class PersonCreateVM
    {
        [Required(ErrorMessage = "Ad sahəsi məcburidir.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad 2 ilə 50 simvol arasında olmalıdır.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad sahəsi məcburidir.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyad 2 ilə 50 simvol arasında olmalıdır.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "E-poçt sahəsi məcburidir.")]
        [EmailAddress(ErrorMessage = "Keçərli bir e-poçt ünvanı daxil edin.")]
        [StringLength(100, ErrorMessage = "E-poçt 100 simvoldan uzun ola bilməz.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Keçərli bir telefon nömrəsi daxil edin.")]
        [StringLength(20, ErrorMessage = "Telefon nömrəsi 20 simvoldan uzun ola bilməz.")]
        public string Phone { get; set; } 

        [Required(ErrorMessage = "Rol sahəsi məcburidir.")]
        [StringLength(50, ErrorMessage = "Rol 50 simvoldan uzun ola bilməz.")]
        public string Role { get; set; }
    }
}
