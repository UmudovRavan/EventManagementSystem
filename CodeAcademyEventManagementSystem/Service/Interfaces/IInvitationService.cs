﻿using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Enums;
using CodeAcademyEventManagementSystem.ViewModels.Invitation;

namespace CodeAcademyEventManagementSystem.Service.Interfaces
{
    public interface IInvitationService : IGenericService<InvitationVM, Invitation>
    {
        Task<InvitationVM> CreateInvitationAsync(InvitationCreateVM model);
        Task<bool> UpdateInvitationStatusAsync(int invitationId, InvitationStatus status);
        Task<IEnumerable<InvitationVM>> GetInvitationsForPersonAsync(int personId);
        Task<IEnumerable<InvitationVM>> GetInvitationsForEventAsync(int eventId);
        Task<InvitationVM> GetInvitationWithDetailsByIdAsync(int id, bool asNoTracking = false);
    }
}