using Microsoft.AspNetCore.Mvc;

namespace MiddleMan.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
