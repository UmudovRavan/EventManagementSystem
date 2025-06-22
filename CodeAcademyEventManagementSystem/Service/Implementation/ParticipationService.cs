using AutoMapper;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Repository.Interface;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using CodeAcademyEventManagementSystem.ViewModels.Participation;

namespace CodeAcademyEventManagementSystem.Service.Implementation
{
    public class ParticipationService : GenericService<ParticipationVM, Participation>, IParticipationService
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMapper _mapper;

        public ParticipationService(
            IParticipationRepository participationRepository,
            IInvitationRepository invitationRepository,
            IMapper mapper)
            : base(participationRepository, mapper)
        {
            _participationRepository = participationRepository;
            _invitationRepository = invitationRepository;
            _mapper = mapper;
        }

        public async Task<ParticipationVM> CreateParticipationAsync(int invitationId)
        {
            var allInvitationsWithDetails = await _invitationRepository.GetInvitationsWithEventAndPersonAsync();
            var invitation = allInvitationsWithDetails.FirstOrDefault(i => i.Id == invitationId);

            var existingParticipation = await _participationRepository.GetParticipationByInvitationIdAsync(invitationId);
            if (existingParticipation != null)
            {
                throw new InvalidOperationException("Bu davetiye için zaten bir check-in yapılmıştır.");
            }

            var participation = new Participation
            {
                InvitationId = invitationId,
                CheckInTime = DateTime.Now,
                SeatNumber = GenerateSeatNumber()
            };
            var result = await _participationRepository.AddAsync(participation);
            await _participationRepository.SaveChangesAsync();
            return _mapper.Map<ParticipationVM>(result);
        }

        private string GenerateSeatNumber() 
        {
           
            return "A" + new Random().Next(1, 100).ToString();
        }

        public async Task<bool> HasPersonCheckedInForEvent(int personId, int eventId)
        {
            var allParticipationsWithDetails = await _participationRepository.GetParticipationsWithInvitationDetailsAsync();
            return allParticipationsWithDetails.Any(p =>
                p.Invitation != null &&
                p.Invitation.PersonId == personId &&
                p.Invitation.EventId == eventId &&
                p.CheckInTime != null);
        }

        public async Task<IEnumerable<ParticipationVM>> GetAllParticipationsWithDetailsAsync()
        {
            var participations = await _participationRepository.GetParticipationsWithInvitationDetailsAsync();
            return _mapper.Map<IEnumerable<ParticipationVM>>(participations);
        }
    }
}
