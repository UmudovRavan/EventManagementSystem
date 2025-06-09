using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Person;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class PersonService : GenericService<PersonVM, Person>, IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
            : base(personRepository, mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<PersonVM> GetPersonByEmailAsync(string email)
        {
            var personEntity = await _personRepository.GetPersonByEmailAsync(email);
            return _mapper.Map<PersonVM>(personEntity);
        }

        public async Task<IEnumerable<PersonVM>> GetPersonsByRoleAsync(string role)
        {
            var persons = await _personRepository.GetPersonsByRoleAsync(role);
            return _mapper.Map<IEnumerable<PersonVM>>(persons);
        }
    }
}
