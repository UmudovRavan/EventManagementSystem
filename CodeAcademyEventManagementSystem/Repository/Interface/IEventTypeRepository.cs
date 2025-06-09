using CodeAcademyEventManagementSystem.Entities;

namespace CodeAcademyEventManagementSystem.Repository.Interface
{
    public interface IEventTypeRepository : IGenericRepository<EventType>
    {
        Task<EventType> GetEventTypeByNameAsync(string name);
    }
}
