using Microsoft.AspNetCore.Identity;

namespace CodeAcademyEventManagementSystem.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Role { get; set; }
    }
}
