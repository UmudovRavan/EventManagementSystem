using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Event;
using CodeAcademyEventManagementSystem.ViewModels.Invitation;
using CodeAcademyEventManagementSystem.ViewModels.Person;
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
        private readonly IPersonService _personService;
        private readonly IInvitationService _invitationService;

        public EventController(
            IEventService eventService,
            ILocationService locationService,
            IEventTypeService eventTypeService,
            IOrganizerService organizerService,
            IPersonService personService,
            IInvitationService invitationService)
        {
            _eventService = eventService;
            _locationService = locationService;
            _eventTypeService = eventTypeService;
            _organizerService = organizerService;
            _personService = personService;
            _invitationService = invitationService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEventsWithDetailsAsync();
            return View(events);
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventModel = await _eventService.GetEventByIdWithDetailsAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }

            var invitations = await _invitationService.GetInvitationsForEventAsync(id) ?? Enumerable.Empty<InvitationVM>();
            var invitedPersonIds = invitations.Select(i => i.PersonId).ToList();

            var allUsers = await _personService.GetAllAsync() ?? Enumerable.Empty<PersonVM>();
            var usersToInvite = allUsers.Where(u => !invitedPersonIds.Contains(u.Id));

            // Təhlükəsiz SelectList yaratmaq üçün
            var userSelectList = usersToInvite.Select(u => new
            {
                Id = u.Id,
                FullName = u.Name + " " + u.Surname
            }).ToList();


            ViewBag.Invitations = invitations;
            ViewBag.UsersToInvite = new SelectList(userSelectList, "Id", "FullName");

            return View(eventModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Invite(int eventId, int personIdToInvite)
        {
            if (personIdToInvite > 0)
            {
                var invitationModel = new InvitationCreateVM
                {
                    EventId = eventId,
                    PersonId = personIdToInvite
                };
                await _invitationService.CreateInvitationAsync(invitationModel);
                TempData["SuccessMessage"] = "Dəvət uğurla göndərildi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Zəhmət olmasa, dəvət üçün istifadəçi seçin.";
            }

            return RedirectToAction("Details", new { id = eventId });
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

            ViewBag.Locations = new SelectList(locations, "Id", "Name");
            ViewBag.EventTypes = new SelectList(eventTypes, "Id", "Name");
            ViewBag.Organizers = new SelectList(organizers, "Id", "FullName");
        }
    }
}