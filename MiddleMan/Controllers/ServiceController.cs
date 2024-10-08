using Microsoft.AspNetCore.Mvc;

namespace MiddleMan.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Service()
        {
            return View();
        }
    }
}
