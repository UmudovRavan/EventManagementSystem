using CodeAcademyEventManagementSystem.Entities;

namespace CodeAcademyEventManagementSystem.Repository.Interface
{
    public interface IPersonRepository: IGenericRepository<Person>
    {
        Task<Person> GetPersonByEmailAsync(string email);
        Task<IEnumerable<Person>> GetPersonsByRoleAsync(string role);
    }
}
