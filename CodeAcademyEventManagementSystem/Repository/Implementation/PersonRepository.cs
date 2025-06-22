using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Repository.Implementation
{
    public class PersonRepository: GenericRepository<Person>, IPersonRepository
    {
        private readonly EventSystemDB _context;
        public PersonRepository(EventSystemDB context) : base(context) => _context = context;

        public async Task<Person> GetPersonByEmailAsync(string email)
        {
            return await _context.Persons.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email && !p.IsDeleted);
        }

        public async Task<IEnumerable<Person>> GetPersonsByRoleAsync(string role)
        {
            return await _context.Persons.Where(p => p.Role == role && !p.IsDeleted).AsNoTracking().ToListAsync();
        }
    }
}
