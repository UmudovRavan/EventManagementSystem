using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Organizer;
using Microsoft.AspNetCore.Mvc;

namespace CodeAcademyEventManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrganizerController : Controller
    {
        private readonly IOrganizerService _organizerService;
        public OrganizerController(IOrganizerService organizerService)=> _organizerService = organizerService;
        public async Task<IActionResult> Index()
        {
            var organizers = await _organizerService.GetAllAsync();
            return View(organizers);
        }
        public IActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrganizerCreateVM model)
        {
            if (!ModelState.IsValid)                return View(model);
            await _organizerService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var organizer = await _organizerService.GetByIdAsync(id);
            if (organizer == null)           return NotFound();
            var model = new OrganizerEditVM
            {
                Id = organizer.Id,
                FullName = organizer.FullName,
                Email = organizer.Email
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrganizerEditVM model)
        {
            if (!ModelState.IsValid)                return View(model);
            await _organizerService.Update(model);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var organizer = await _organizerService.GetByIdAsync(id);
            if (organizer == null)                return NotFound();
            return View(organizer);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _organizerService.DeleteAsync(id);
            if (!success)                return NotFound();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var organizer = await _organizerService.GetByIdAsync(id);
            if (organizer == null)                return NotFound();
            return View(organizer);
        }
    }
}
