using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Repository.Implementation
{
    public class OrganizerRepository : GenericRepository<Organizer>, IOrganizerRepository
    {
        private readonly EventSystemDB _context; 
        public OrganizerRepository(EventSystemDB context) : base(context) => _context = context;
        public async Task<Organizer> GetOrganizerByEmailAsync(string email)
        {
            return await _context.Organizers
                                 .AsNoTracking() 
                                 .FirstOrDefaultAsync(o => o.Email == email && !o.IsDeleted); 
        }
    }
}
