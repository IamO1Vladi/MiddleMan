using Microsoft.AspNetCore.Mvc;
using MiddleMan.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Abstractions;
using MiddleManServices.ApiServices.QuickBase.Interfaces;
using MiddleManServices.ApiServices.QuickBase.ServiceModels;

namespace MiddleMan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQuickBaseService quickBaseService;

        public HomeController(ILogger<HomeController> logger,IQuickBaseService quickBaseService)
        {
            _logger = logger;
            this.quickBaseService = quickBaseService;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendGetInTouchMessage(GetInTouchServiceModel formInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await quickBaseService.SendGetInTouchMessage(formInfo);
                    return Json(new { success = true, message = "Message sent successfully. We will reach out back to you shortly." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "There was an error sending your message. Please try again later." });
                }
            }

            return Json(new { success = false, message = "Invalid data. Please check your inputs and try again." });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
