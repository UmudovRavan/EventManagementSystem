
using CodeAcademyEventManagementSystem.Areas.Admin.Models;
using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CreditManagementSystemHomework.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly EventSystemDB _dbContext;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, EventSystemDB dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError("", "İstifadəçi adı və ya şifrə yanlışdır.");
                return View(model);
            }

            if (!user.IsApproved)
            {
                ModelState.AddModelError("", "Hesabınız hələ admin tərəfindən təsdiqlənməyib. Zəhmət olmasa admin təsdiqini gözləyin.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);

            if (result.Succeeded)
            {
                user.LastLoginIpAdr = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmir";
                await _userManager.UpdateAsync(user);

                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Hesabınız bloklanıb. Bir az sonra cəhd edin.");
                return View(model);
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError("", "Hesabınız aktiv deyil. Lütfən e-poçtunuzu təsdiqləyin və ya admin təsdiqini gözləyin.");
                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "İstifadəçi adı və ya şifrə yanlışdır.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            var model = new SignUpVM
            {
                AvailablePersonRoles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Tələbə", Text = "Tələbə" },
                    new SelectListItem { Value = "Müəllim", Text = "Müəllim" },
                    new SelectListItem { Value = "Qonaq", Text = "Qonaq" }
                }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM signUpVM)
        {
            if (!ModelState.IsValid)
            {
                signUpVM.AvailablePersonRoles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Tələbə", Text = "Tələbə" },
                    new SelectListItem { Value = "Müəllim", Text = "Müəllim" },
                    new SelectListItem { Value = "Qonaq", Text = "Qonaq" }
                };
                return View(signUpVM);
            }

            var remoteIpAdress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Bilinmir";
            var user = new ApplicationUser
            {
                UserName = signUpVM.Username,
                Email = signUpVM.Email,
                EmailConfirmed = false,
                IsApproved = false,
                LastLoginIpAdr = remoteIpAdress
            };

            var createResult = await _userManager.CreateAsync(user, signUpVM.Password);
            if (createResult.Succeeded)
            {
                var person = new Person
                {
                    Name = signUpVM.Username,
                    Surname = "",
                    Email = signUpVM.Email,
                    Phone = "",
                    Role = signUpVM.DesiredPersonRole,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                };
                _dbContext.Persons.Add(person);
                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Qeydiyyatınız uğurla ta   mamlandı. Hesabınız admin tərəfindən təsdiqləndikdən sonra daxil ola bilərsiniz.";
                return RedirectToAction("SignIn", "Account", new { area = "Admin" });
            }
            else
            {
                foreach (var item in createResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            signUpVM.AvailablePersonRoles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Tələbə", Text = "Tələbə" },
                new SelectListItem { Value = "Müəllim", Text = "Müəllim" },
                new SelectListItem { Value = "Qonaq", Text = "Qonaq" }
            };
            return View(signUpVM);
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Account", new { area = "Admin" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutMain()
        {
            await HttpContext.SignOutAsync(); 
            return RedirectToAction("Index", "Home", new { area = "" }); 
        }
    }
}
