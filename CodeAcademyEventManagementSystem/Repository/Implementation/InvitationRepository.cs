using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Repository.Implementation
{
    public class InvitationRepository : GenericRepository<Invitation>, IInvitationRepository
    {
        private readonly EventSystemDB _context;
        public InvitationRepository(EventSystemDB context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Invitation>> GetInvitationsWithEventAndPersonAsync()
        {
            return await _context.Invitations
                .Include(i => i.Event)
                .Include(i => i.Person)
                .ToListAsync();
        }
    }
}
