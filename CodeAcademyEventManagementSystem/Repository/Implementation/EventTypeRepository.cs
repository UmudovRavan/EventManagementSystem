using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Repository.Implementation
{
    public class EventTypeRepository : GenericRepository<EventType>, IEventTypeRepository
    {
        private readonly EventSystemDB _context; 
        public EventTypeRepository(EventSystemDB context) : base(context)=> _context = context;
        public async Task<EventType> GetEventTypeByNameAsync(string name)
        {
            return await _context.EventTypes
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(et => et.Name == name && !et.IsDeleted); 
        }

    }
}
