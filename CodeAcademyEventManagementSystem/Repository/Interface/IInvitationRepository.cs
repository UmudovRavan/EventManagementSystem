using CodeAcademyEventManagementSystem.Entities;

namespace CodeAcademyEventManagementSystem.Repository.Interface
{
    public interface IInvitationRepository : IGenericRepository<Invitation>
    {
        Task<IEnumerable<Invitation>> GetInvitationsWithEventAndPersonAsync();
    }
}
