using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.ViewModels.Person;

namespace CodeAcademyEventManagementSystem.Service.Interfaces
{
    public interface IPersonService : IGenericService<PersonVM, Person>
    {
        Task<PersonVM> GetPersonByEmailAsync(string email);
        Task<IEnumerable<PersonVM>> GetPersonsByRoleAsync(string role); // Yeni eklendi
    }
}
