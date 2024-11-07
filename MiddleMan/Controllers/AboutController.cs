using Microsoft.AspNetCore.Mvc;

namespace MiddleMan.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }

        public IActionResult CookiesStatement()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
