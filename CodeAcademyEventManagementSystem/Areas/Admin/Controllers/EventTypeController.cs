using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.EventType;
using Microsoft.AspNetCore.Mvc;

namespace CodeAcademyEventManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventTypeController : Controller
    {
        private readonly IEventTypeService _eventTypeService;
        private readonly IMapper _mapper;
        public EventTypeController(IEventTypeService eventTypeService, IMapper mapper)
        {
            _eventTypeService = eventTypeService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var eventTypes = await _eventTypeService.GetAllAsync();
            return View(eventTypes);
        }
        public IActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventTypeCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var eventTypeVM = _mapper.Map<EventTypeVM>(vm);
            await _eventTypeService.CreateAsync(eventTypeVM);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var eventType = await _eventTypeService.GetByIdAsync(id);
            if (eventType == null) return NotFound();
            var editVM = _mapper.Map<EventTypeEditVM>(eventType);
            return View(editVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventTypeEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var eventTypeEntity = _mapper.Map<EventType>(vm);
            await _eventTypeService.Update(eventTypeEntity);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _eventTypeService.DeleteAsync(id);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
