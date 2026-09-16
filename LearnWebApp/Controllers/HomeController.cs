using Microsoft.AspNetCore.Mvc;

namespace LearnWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
