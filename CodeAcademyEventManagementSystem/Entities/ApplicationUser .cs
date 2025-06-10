using Microsoft.AspNetCore.Identity;

namespace CodeAcademyEventManagementSystem.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string LastLoginIpAdr { get; set; }
        public bool IsApproved { get; set; } = false;
    }
}
