using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Repository.Implementation
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private readonly EventSystemDB _context;
        public FeedbackRepository(EventSystemDB context) : base(context) => _context = context;
        public async Task<IEnumerable<Feedback>> GetFeedbacksWithDetailsAsync()
        {
            return await _context.Feedbacks
                .Include(f => f.Event)
                .Include(f => f.Person)
                .ToListAsync();
        }
    }
}
