using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.Enums;
using CodeAcademyEventManagementSystem.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeAcademyEventManagementSystem.Controllers
{
    [Authorize]
    public class InvitationController : Controller
    {
        private readonly IInvitationService _invitationService;
        private readonly IPersonService _personService;
        private readonly UserManager<ApplicationUser> _userManager;

        public InvitationController(
            IInvitationService invitationService,
            IPersonService personService,
            UserManager<ApplicationUser> userManager)
        {
            _invitationService = invitationService;
            _personService = personService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var person = await _personService.GetPersonByEmailAsync(currentUser.Email);
            if (person == null)
            {
                return View(Enumerable.Empty<ViewModels.Invitation.InvitationVM>());
            }

            var invitations = await _invitationService.GetInvitationsForPersonAsync(person.Id);
            return View(invitations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int id)
        {
            await UpdateInvitationStatus(id, InvitationStatus.Accepted);
            TempData["SuccessMessage"] = "Dəvət uğurla qəbul edildi.";
            return Redirect("/Invitation/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Decline(int id)
        {
            await UpdateInvitationStatus(id, InvitationStatus.Rejected);
            TempData["SuccessMessage"] = "Dəvət rədd edildi.";
            return Redirect("/Invitation/Index");
        }

        private async Task UpdateInvitationStatus(int invitationId, InvitationStatus status)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return;

            var person = await _personService.GetPersonByEmailAsync(currentUser.Email);
            if (person == null) return;

            var invitation = await _invitationService.GetInvitationWithDetailsByIdAsync(invitationId);
            if (invitation == null || invitation.PersonId != person.Id) return;

            await _invitationService.UpdateInvitationStatusAsync(invitationId, status);
        }
    }
}
