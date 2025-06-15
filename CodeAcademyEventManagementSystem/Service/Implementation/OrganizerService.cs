using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Organizer;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class OrganizerService : GenericService<OrganizerVM, Organizer>, IOrganizerService
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IMapper _mapper;

        public OrganizerService(IOrganizerRepository organizerRepository, IMapper mapper)
            : base(organizerRepository, mapper)
        {
            _organizerRepository = organizerRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(OrganizerCreateVM model)
        {
            var organizerEntity = _mapper.Map<Organizer>(model);
            await _organizerRepository.AddAsync(organizerEntity);
            await _organizerRepository.SaveChangesAsync();
        }

        public async Task<OrganizerVM> GetOrganizerByEmailAsync(string email)
        {
            var organizerEntity = await _organizerRepository.GetOrganizerByEmailAsync(email);
            return _mapper.Map<OrganizerVM>(organizerEntity);
        }

        public async Task Update(OrganizerEditVM model)
        {
            var organizerEntity = _mapper.Map<Organizer>(model);
            await _organizerRepository.UpdateAsync(organizerEntity);
            await _organizerRepository.SaveChangesAsync();
        }
    }
}
