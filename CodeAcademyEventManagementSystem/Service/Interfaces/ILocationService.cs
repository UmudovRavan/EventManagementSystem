using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.ViewModels.Location;

namespace CodeAcademyEventManagementSystem.Service.Interfaces
{
    public interface ILocationService : IGenericService<LocationVM, Location>
    {
        Task<IEnumerable<LocationVM>> GetLocationsWithCapacityGreaterThan(int capacity);
    }
}
