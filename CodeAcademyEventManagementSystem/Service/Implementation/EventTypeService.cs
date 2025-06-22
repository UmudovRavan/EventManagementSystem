using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.EventType;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class EventTypeService : GenericService<EventTypeVM, EventType>, IEventTypeService
    {
        private readonly IEventTypeRepository _eventTypeRepository;
        private readonly IMapper _mapper;
        public EventTypeService(IEventTypeRepository eventTypeRepository, IMapper mapper): base(eventTypeRepository, mapper)
        {
            _eventTypeRepository = eventTypeRepository;
            _mapper = mapper;
        }
        public async Task<EventTypeVM> GetEventTypeByNameAsync(string name)
        {
            var eventTypeEntity = await _eventTypeRepository.GetEventTypeByNameAsync(name);
            return _mapper.Map<EventTypeVM>(eventTypeEntity);
        }
        public async Task Update(EventType eventTypeEntity)
        {
            await _eventTypeRepository.UpdateAsync(eventTypeEntity);
            await _eventTypeRepository.SaveChangesAsync();
        }
    }
}
