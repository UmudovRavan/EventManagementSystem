using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Repository.Implementation
{
    public class NotificationRepository : GenericRepository<Notification> ,INotificationRepository
    {
        private readonly EventSystemDB _context;
        public NotificationRepository(EventSystemDB context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Notification>> GetNotificationsWithEventAsync()
        {
            return await _context.Notifications
                .Include(n => n.Event)
                .ToListAsync();
        }
    }
}
