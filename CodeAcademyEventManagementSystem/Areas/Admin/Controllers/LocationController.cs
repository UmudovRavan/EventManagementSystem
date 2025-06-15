using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Location;
using Microsoft.AspNetCore.Mvc;

namespace CodeAcademyEventManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        // GET: Location
        public async Task<IActionResult> Index()
        {
            var locations = await _locationService.GetAllAsync();
            return View(locations);
        }

        // GET: Location/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _locationService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: Location/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            var model = new LocationEditVM
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                Capacity = location.Capacity
            };

            return View(model);
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LocationEditVM model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _locationService.Update(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: Location/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _locationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
