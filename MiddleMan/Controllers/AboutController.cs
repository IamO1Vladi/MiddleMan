using Microsoft.AspNetCore.Mvc;

namespace MiddleMan.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
