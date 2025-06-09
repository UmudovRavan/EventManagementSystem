using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.ViewModels.EventType;

namespace CodeAcademyEventManagementSystem.Service.Interfaces
{
    public interface IEventTypeService : IGenericService<EventTypeVM, EventType>
    {
        Task<EventTypeVM> GetEventTypeByNameAsync(string name);
    }
}
