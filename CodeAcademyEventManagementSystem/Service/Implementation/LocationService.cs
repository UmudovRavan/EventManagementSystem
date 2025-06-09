using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Location;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class LocationService : GenericService<LocationVM, Location>, ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
            : base(locationRepository, mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationVM>> GetLocationsWithCapacityGreaterThan(int capacity)
        {
            var locations = await _locationRepository.GetLocationsByCapacityAsync(capacity);
            return _mapper.Map<IEnumerable<LocationVM>>(locations);
        }
    }
}
