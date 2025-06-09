
using CodeAcademyEventManagementSystem.Areas.Admin.Models;
using CodeAcademyEventManagementSystem.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CreditManagementSystemHomework.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account is locked out.");
                return View(model);
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError("", "Your account is not allowed");
                return View(model);
            }

            var errorMessage = "Username or password is incorrect";
            ModelState.AddModelError("", errorMessage);
            return View(model);
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM signUpVM)
        {
            if (!ModelState.IsValid)
            {
                return View(signUpVM);
            }
            var remoteIpAdress = " 192.168.137.1";
            var user = new ApplicationUser
            {
                UserName = signUpVM.Username,
                Email = signUpVM.Email,
                LastLoginIpAdr = remoteIpAdress
            };
            var result =  await _userManager.CreateAsync(user, signUpVM.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); 
            return RedirectToAction("SignIn", "Account", new { area = "Admin" });
        }
    }
}
