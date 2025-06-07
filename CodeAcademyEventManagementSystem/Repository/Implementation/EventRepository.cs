using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Repository.Implementation
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly EventSystemDB _context;
        public EventRepository(EventSystemDB context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Event>> GetAllWithDetailsAsync()
        {
            return await _context.Events
                .Include(e=>e.Location)
                .Include(e => e.EventType)
                .Include(e=> e.Organizer)
                .ToListAsync();
        }
    }
}
