using Microsoft.AspNetCore.Mvc;

namespace CodeAcademyEventManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
