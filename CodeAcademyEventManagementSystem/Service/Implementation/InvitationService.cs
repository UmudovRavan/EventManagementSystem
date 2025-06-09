using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Invitation;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class InvitationService : GenericService<InvitationVM, Invitation>, IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMapper _mapper;

        public InvitationService(IInvitationRepository invitationRepository, IMapper mapper)
            : base(invitationRepository, mapper)
        {
            _invitationRepository = invitationRepository;
            _mapper = mapper;
        }

        public async Task<InvitationVM> CreateInvitationAsync(InvitationCreateVM model)
        {
            var invitation = _mapper.Map<Invitation>(model);
            invitation.SentAt = DateTime.Now;
            invitation.Status = InvitationStatus.Pending.ToString(); // Enum'u string'e çevir
            var result = await _invitationRepository.AddAsync(invitation);
            await _invitationRepository.SaveChangesAsync();
            return _mapper.Map<InvitationVM>(result);
        }

        public async Task<bool> UpdateInvitationStatusAsync(int invitationId, InvitationStatus status)
        {
            var invitation = await _invitationRepository.GetByIdAsync(invitationId);
            if (invitation == null)
            {
                return false;
            }

            invitation.Status = status.ToString(); // Enum'u string'e çevir
            await _invitationRepository.UpdateAsync(invitation);
            await _invitationRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InvitationVM>> GetInvitationsForPersonAsync(int personId)
        {
            var allInvitationsWithDetails = await _invitationRepository.GetInvitationsWithEventAndPersonAsync();
            var filteredInvitations = allInvitationsWithDetails.Where(i => i.PersonId == personId);
            return _mapper.Map<IEnumerable<InvitationVM>>(filteredInvitations);
        }

        public async Task<IEnumerable<InvitationVM>> GetInvitationsForEventAsync(int eventId)
        {
            var allInvitationsWithDetails = await _invitationRepository.GetInvitationsWithEventAndPersonAsync();
            var filteredInvitations = allInvitationsWithDetails.Where(i => i.EventId == eventId);
            return _mapper.Map<IEnumerable<InvitationVM>>(filteredInvitations);
        }

        public async Task<InvitationVM> GetInvitationWithDetailsByIdAsync(int id)
        {
            // InvitationRepository'de ID'ye göre detayları getiren spesifik bir metot olmadığı için
            // GetInvitationsWithEventAndPersonAsync'i kullanıp memory'de filtreleme yapıyoruz.
            var allInvitationsWithDetails = await _invitationRepository.GetInvitationsWithEventAndPersonAsync();
            var invitationEntity = allInvitationsWithDetails.FirstOrDefault(i => i.Id == id);
            return _mapper.Map<InvitationVM>(invitationEntity);
        }
    }
}
