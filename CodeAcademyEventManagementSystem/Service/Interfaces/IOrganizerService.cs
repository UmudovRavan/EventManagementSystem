using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.ViewModels.Organizer;

namespace CodeAcademyEventManagementSystem.Service.Interfaces
{
    public interface IOrganizerService : IGenericService<OrganizerVM, Organizer>
    {
        Task<OrganizerVM> GetOrganizerByEmailAsync(string email);
    }
}
