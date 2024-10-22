using Microsoft.AspNetCore.Mvc;

namespace MiddleMan.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult FindASupplier()
        {
            return View("FindASupplierService");
        }

        public IActionResult OrganizeAFactoryTour()
        {
            return View("OrganizeAFactoryTourService");
        }

        public IActionResult TalentDevelopment()
        {
            return View("TalentDevelopmentService");
        }

        public IActionResult ExpandYourBusiness()
        {
            return View("ExpandYourBusinessService");
        }
    }
}
