using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeAcademyEventManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ILocationService _locationService;
        private readonly IEventTypeService _eventTypeService;
        private readonly IOrganizerService _organizerService;

        public EventController(
            IEventService eventService,
            ILocationService locationService,
            IEventTypeService eventTypeService,
            IOrganizerService organizerService)
        {
            _eventService = eventService;
            _locationService = locationService;
            _eventTypeService = eventTypeService;
            _organizerService = organizerService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEventsWithDetailsAsync();
            return View(events);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateVM eventVM)
        {
            if (!ModelState.IsValid)
            {
                await _eventService.CreateEventAsync(eventVM);
                return RedirectToAction(nameof(Index));
            }

            // Əgər validation uğursuzdursa, dropdown-ları yenidən doldur
            await PopulateDropdowns();
            return View(eventVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var eventToEdit = await _eventService.GetEventByIdWithDetailsAsync(id);
            if (eventToEdit == null)
            {
                return NotFound();
            }

            // EventEditVM yaradılır və doldurulur
            var model = new EventEditVM
            {
                Id = eventToEdit.Id,
                Title = eventToEdit.Title,
                Description = eventToEdit.Description,
                Date = eventToEdit.Date,
                LocationId = eventToEdit.LocationId,
                EventTypeId = eventToEdit.EventTypeId,
                OrganizerId = eventToEdit.OrganizerId
            };

            await PopulateDropdowns(); // dropdownları model-ə əlavə edir

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventEditVM model)
        {
            if (!ModelState.IsValid)
            {
                await _eventService.UpdateEventAsync(model);
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdowns();
            return View(model);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var eventToDelete = await _eventService.GetByIdAsync(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }
            return View(eventToDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eventService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdowns()
        {
            var locations = await _locationService.GetAllAsync();
            var eventTypes = await _eventTypeService.GetAllAsync();
            var organizers = await _organizerService.GetAllAsync();

            ViewBag.Locations = locations.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name
            });

            ViewBag.EventTypes = eventTypes.Select(et => new SelectListItem
            {
                Value = et.Id.ToString(),
                Text = et.Name
            });

            ViewBag.Organizers = organizers.Select(o => new SelectListItem
            {
                Value = o.Id.ToString(),
                Text = o.FullName
            });
        }
    }
}
