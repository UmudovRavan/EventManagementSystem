using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Event;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class EventService : GenericService<EventVM, Event>, IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
            : base(eventRepository, mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
            
        public async Task<IEnumerable<EventVM>> GetAllEventsWithDetailsAsync()
        {
            var events = await _eventRepository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<EventVM>>(events);
        }

        public async Task<EventVM> GetEventByIdWithDetailsAsync(int id)
        {
            var allEventsWithDetails = await _eventRepository.GetAllWithDetailsAsync();
            var eventEntity = allEventsWithDetails.FirstOrDefault(e => e.Id == id);
            return _mapper.Map<EventVM>(eventEntity);
        }

        public async Task<EventVM> CreateEventAsync(EventCreateVM model)
        {
            var entity = _mapper.Map<Event>(model);
            var result = await _eventRepository.AddAsync(entity);
            await _eventRepository.SaveChangesAsync(); 
            return _mapper.Map<EventVM>(result);
        }

        public async Task<EventVM> UpdateEventAsync(EventEditVM model)
        {
            var entity = _mapper.Map<Event>(model);
            var updatedEntity = await _eventRepository.UpdateAsync(entity);
            await _eventRepository.SaveChangesAsync(); // Benzer şekilde
            return _mapper.Map<EventVM>(updatedEntity);
        }
    }
}
