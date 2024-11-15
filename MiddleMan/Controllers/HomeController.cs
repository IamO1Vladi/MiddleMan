using Microsoft.AspNetCore.Mvc;
using MiddleMan.Models;
using System.Diagnostics;
using MiddleMan.Common.Constants;
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
                    return Json(new { success = true, message = TextConstants.GetInTouchSuccessMessage});
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = TextConstants.GetInTouchFailedMessage });
                }
            }

            return Json(new { success = false, message = TextConstants.GetInTouchInvalidDataError });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Error/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
         
                Response.StatusCode = statusCode; 
                return View("NotFound"); 
                
        }
    }
}
