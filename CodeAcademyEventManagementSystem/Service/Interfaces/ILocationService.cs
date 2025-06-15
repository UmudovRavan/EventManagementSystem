using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.ViewModels.Location;

namespace CodeAcademyEventManagementSystem.Service.Interfaces
{
    public interface ILocationService : IGenericService<LocationVM, Location>
    {
        Task CreateAsync(LocationCreateVM model);
        Task<IEnumerable<LocationVM>> GetLocationsWithCapacityGreaterThan(int capacity);
        Task Update(LocationEditVM model);
    }
}
