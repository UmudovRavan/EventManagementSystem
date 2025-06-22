using AutoMapper;
using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Enums;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Invitation;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class InvitationService : GenericService<InvitationVM, Invitation>, IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMapper _mapper;
        private readonly EventSystemDB _context;

        public InvitationService(IInvitationRepository invitationRepository, IMapper mapper, EventSystemDB context)
            : base(invitationRepository, mapper)
        {
            _invitationRepository = invitationRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<InvitationVM> CreateInvitationAsync(InvitationCreateVM model)
        {
            var invitation = _mapper.Map<Invitation>(model);
            invitation.SentAt = DateTime.UtcNow;
            invitation.Status = InvitationStatus.Pending;

            var result = await _invitationRepository.AddAsync(invitation);
            await _invitationRepository.SaveChangesAsync();
            return _mapper.Map<InvitationVM>(result);
        }

        public async Task<bool> UpdateInvitationStatusAsync(int invitationId, InvitationStatus status)
        {
            var invitation = await _invitationRepository.GetByIdAsync(invitationId);
            if (invitation == null)
                return false;

            invitation.Status =InvitationStatus.Accepted;

            await _context.SaveChangesAsync(); 

            return true;
        }

        public async Task<IEnumerable<InvitationVM>> GetInvitationsForPersonAsync(int personId)
        {
            var allInvitations = await _invitationRepository.GetInvitationsWithEventAndPersonAsync();
            var filtered = allInvitations.Where(i => i.PersonId == personId);
            return _mapper.Map<IEnumerable<InvitationVM>>(filtered);
        }

        public async Task<IEnumerable<InvitationVM>> GetInvitationsForEventAsync(int eventId)
        {
            var allInvitations = await _invitationRepository.GetInvitationsWithEventAndPersonAsync();
            var filtered = allInvitations.Where(i => i.EventId == eventId);
            return _mapper.Map<IEnumerable<InvitationVM>>(filtered);
        }

        public async Task<InvitationVM> GetInvitationWithDetailsByIdAsync(int id, bool asNoTracking = false)
        {
            var allInvitations = await _invitationRepository.GetInvitationsWithEventAndPersonAsync();

            var invitation = allInvitations.FirstOrDefault(i => i.Id == id);

            if (invitation == null) return null;


            return _mapper.Map<InvitationVM>(invitation);
        }
    }
}
