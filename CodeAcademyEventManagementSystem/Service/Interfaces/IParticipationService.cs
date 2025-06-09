using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.ViewModels.Participation;

namespace CodeAcademyEventManagementSystem.Service.Interfaces
{
    public interface IParticipationService : IGenericService<ParticipationVM, Participation>
    {
        Task<ParticipationVM> CreateParticipationAsync(int invitationId);
        Task<bool> HasPersonCheckedInForEvent(int personId, int eventId);
        Task<IEnumerable<ParticipationVM>> GetAllParticipationsWithDetailsAsync(); // Yeni
    }
}
