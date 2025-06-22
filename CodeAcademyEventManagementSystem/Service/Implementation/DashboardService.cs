using CodeAcademyEventManagementSystem.Areas.Admin.Models;
using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Event;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly EventSystemDB  _context;

        public DashboardService(EventSystemDB context) => _context = context;
        public async Task<DashboardVM> GetDashboardDataAsync()
        {
            var events = await _context.Events
                .Include(e => e.Location)
                .Include(e => e.EventType)
                .Include(e => e.Organizer)
                .ToListAsync();
            var locationGroups = events
                .GroupBy(e => e.Location.Name)
                .Select(g => new LocationStatVM
                {
                    Name = g.Key,
                    EventCount = g.Count(),
                    Percentage = 100.0 * g.Count() / events.Count
                }).ToList();
            var eventVMs = events.Select(e => new EventVM
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date, 
                LocationName = e.Location.Name,
                EventTypeName = e.EventType.Name,
                OrganizerName = e.Organizer.FullName, 
                Status = e.IsActive ? "Active" : "Pending",
                StatusColor = e.IsActive ? "success" : "warning"              
            }).ToList();
            return new DashboardVM
            {
                LocationStats = locationGroups,
                CurrentEvents = eventVMs
            };
        }
    }
}
