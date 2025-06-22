using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;

namespace CodeAcademyEventManagementSystem.Areas.Admin.ViewComponents
{
    public class CurrentEventsViewComponent : ViewComponent
    {
        private readonly IEventService _eventService;
        public CurrentEventsViewComponent(IEventService eventService)=> _eventService = eventService;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var events = await _eventService.GetCurrentEventsAsync();
            var currentEvents = events
                .Select(e => new CurrentEventVM
                {
                    Id = e.Id,
                    Title = e.Title,
                    Date = e.Date
                }).ToList();
            return View(currentEvents);
        }
    }
}
