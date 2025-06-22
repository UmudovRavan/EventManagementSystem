using System.ComponentModel.DataAnnotations;

namespace CodeAcademyEventManagementSystem.Areas.Admin.Models
{
    public class UserApprovalVM
    {
        public string Id { get; set; } 
        public string Username { get; set; }
        public string Email { get; set; }
        [Display(Name = "Müraciət Edilən Rol")]
        public string DesiredPersonRole { get; set; }
        [Display(Name = "Qeydiyyat Tarixi")]
        public DateTime RegisteredAt { get; set; } 
        [Display(Name = "Təsdiqlənib")]
        public bool IsApproved { get; set; }
    }
}
