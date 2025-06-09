using CodeAcademyEventManagementSystem.Entities;

namespace CodeAcademyEventManagementSystem.Repository.Interface
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        Task<IEnumerable<Location>> GetLocationsByCapacityAsync(int capacity);
    }
}
