using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.ViewModels.Organizer;

namespace CodeAcademyEventManagementSystem.Service.Interfaces
{
    public interface IOrganizerService : IGenericService<OrganizerVM, Organizer>
    {
        Task CreateAsync(OrganizerCreateVM model);
        Task<OrganizerVM> GetOrganizerByEmailAsync(string email);
        Task Update(OrganizerEditVM model);
    }
}
