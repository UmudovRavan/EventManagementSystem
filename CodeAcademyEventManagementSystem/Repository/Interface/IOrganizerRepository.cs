using CodeAcademyEventManagementSystem.Entities;

namespace CodeAcademyEventManagementSystem.Repository.Interface
{
    public interface IOrganizerRepository : IGenericRepository<Organizer>
    {
        Task<Organizer> GetOrganizerByEmailAsync(string email);
    }
}
