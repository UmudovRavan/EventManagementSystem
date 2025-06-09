using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Repository.Implementation
{
    public class ParticipationRepository:GenericRepository<Participation>, IParticipationRepository
    {
        private readonly EventSystemDB _context;
        public ParticipationRepository(EventSystemDB context) : base(context)
        {
            _context = context;
        }

        public async Task<Participation> GetParticipationByInvitationIdAsync(int invitationId)
        {
            return await _context.Participations
                .Include(p => p.Invitation) 
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.InvitationId == invitationId && !p.IsDeleted);
        }

        public async Task<IEnumerable<Participation>> GetParticipationsWithInvitationDetailsAsync()
        {
            return await _context.Participations
                .Include(p => p.Invitation)
                    .ThenInclude(i => i.Event) // Invitation içindeki Event'i de dahil et
                .Include(p => p.Invitation)
                    .ThenInclude(i => i.Person) // Invitation içindeki Person'ı da dahil et
                .Where(p => !p.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
