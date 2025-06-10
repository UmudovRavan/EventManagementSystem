using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.Areas.Admin.Models
{
    public class SignUpVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Müraciət etdiyiniz rolu seçin.")]
        [Display(Name = "Rol")]
        public string DesiredPersonRole { get; set; }
        public IEnumerable<SelectListItem>? AvailablePersonRoles { get; set; }

    }
}
