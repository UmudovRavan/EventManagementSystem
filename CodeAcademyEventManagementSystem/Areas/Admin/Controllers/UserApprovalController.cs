using CodeAcademyEventManagementSystem.Areas.Admin.Models;
using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademyEventManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserApprovalController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EventSystemDB _dbContext;

        public UserApprovalController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            EventSystemDB dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usersToApprove = await _userManager.Users
                                                    .Where(u => !u.IsApproved)
                                                    .ToListAsync();

            var userApprovalList = new List<UserApprovalVM>();

            foreach (var user in usersToApprove)
            {
                var person = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Email == user.Email);

                userApprovalList.Add(new UserApprovalVM
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    DesiredPersonRole = person?.Role ?? "Təyin Edilməyib",
                    IsApproved = user.IsApproved,
                    RegisteredAt = DateTime.Now
                });
            }

            return View(userApprovalList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "İstifadəçi tapılmadı.";
                return RedirectToAction(nameof(Index));
            }

            user.IsApproved = true;
            user.EmailConfirmed = true;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                TempData["ErrorMessage"] = "İstifadəçinin təsdiq statusunu yeniləməkdə xəta baş verdi.";
                return RedirectToAction(nameof(Index));
            }

            var person = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Email == user.Email);
            if (person != null && !string.IsNullOrEmpty(person.Role))
            {
                var roleExists = await _roleManager.RoleExistsAsync(person.Role);
                if (roleExists)
                {
                    var addRoleResult = await _userManager.AddToRoleAsync(user, person.Role);
                    if (!addRoleResult.Succeeded)
                    {
                        TempData["ErrorMessage"] = $"İstifadəçiyə '{person.Role}' rolunu təyin etməkdə xəta baş verdi.";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = $"'{person.Role}' adlı Identity rolu mövcud deyil. Zəhmət olmasa rolu yaradın.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "İstifadəçi üçün Person rolu tapılmadı və ya boşdur. Identity rolu təyin edilmədi.";
            }

            TempData["SuccessMessage"] = $"İstifadəçi '{user.UserName}' uğurla təsdiqləndi və rol təyin edildi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "İstifadəçi tapılmadı.";
                return RedirectToAction(nameof(Index));
            }

            var person = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Email == user.Email);
            if (person != null)
            {
                _dbContext.Persons.Remove(person);
                await _dbContext.SaveChangesAsync();
            }

            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                TempData["ErrorMessage"] = "İstifadəçini silməkdə xəta baş verdi.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = $"İstifadəçi '{user.UserName}' uğurla rədd edildi və silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
