using CodeAcademyEventManagementSystem.Data;
using CodeAcademyEventManagementSystem.Entities;
using CodeAcademyEventManagementSystem.ViewModels.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace CodeAcademyEventManagementSystem.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EventSystemDB _dbContext;

        public UserAccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            EventSystemDB dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new UserSignUpVM
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserSignUpVM model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailablePersonRoles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Tələbə", Text = "Tələbə" },
                    new SelectListItem { Value = "Müəllim", Text = "Müəllim" },
                    new SelectListItem { Value = "Qonaq", Text = "Qonaq" }
                };
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                EmailConfirmed = true,
                IsApproved = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                await _userManager.AddToRoleAsync(user, "User");

                var person = new Person
                {
                    Name = model.Username,
                    Surname = "",
                    Email = model.Email,
                    Phone = "",
                    Role = model.DesiredPersonRole,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                };
                _dbContext.Persons.Add(person);
                await _dbContext.SaveChangesAsync();

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            model.AvailablePersonRoles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Tələbə", Text = "Tələbə" },
                new SelectListItem { Value = "Müəllim", Text = "Müəllim" },
                new SelectListItem { Value = "Qonaq", Text = "Qonaq" }
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            var model = new UserSignInVM();
            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserSignInVM model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "İstifadəçi adı və ya şifrə yanlışdır.");
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
