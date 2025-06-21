using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.ViewModels.Register
{
    public class UserSignUpVM
    {
        [Required(ErrorMessage = "İstifadəçi adı məcburidir.")]
        [Display(Name = "İstifadəçi adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "E-poçt məcburidir.")]
        [EmailAddress(ErrorMessage = "Zəhmət olmasa, düzgün e-poçt daxil edin.")]
        [Display(Name = "E-poçt")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə məcburidir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifrə")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifrənin təkrarı")]
        [Compare("Password", ErrorMessage = "Şifrə və təkrarı eyni deyil.")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "Rol seçimi məcburidir.")]
        [Display(Name = "Rolunuz")]
        public string DesiredPersonRole { get; set; }
        
        public IEnumerable<SelectListItem> AvailablePersonRoles { get; set; }
    }
}
