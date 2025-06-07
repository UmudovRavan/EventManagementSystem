using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;

namespace CodeAcademyEventManagementSystem.Repository.Implementation
{
    public class LocationRepository : GenericRepository<Location>,ILocationRepository
    {
        public LocationRepository(EventSystemDB context) : base(context) { }
    }
}
