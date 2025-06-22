using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Repository.Implementation
{
    public class LocationRepository : GenericRepository<Location>,ILocationRepository
    {
        private readonly EventSystemDB _context; 
        public LocationRepository(EventSystemDB context) : base(context) => _context = context;

        public async Task<IEnumerable<Location>> GetLocationsByCapacityAsync(int capacity)
        {
            return await _context.Locations
                                 .Where(l => l.Capacity >= capacity && !l.IsDeleted)
                                 .AsNoTracking() 
                                 .ToListAsync();
        }
    }
}
