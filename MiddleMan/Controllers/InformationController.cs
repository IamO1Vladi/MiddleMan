using Microsoft.AspNetCore.Mvc;
using MiddleMan.Models;
using MiddleManServices.ApiServices.QuickBase.Interfaces;
using MiddleManServices.ApiServices.QuickBase.ServiceModels;

namespace MiddleMan.Controllers
{
    public class InformationController : Controller
    {

        private readonly IQuickBaseService quickBaseService;
        public InformationController(IQuickBaseService quickBaseService)
        {
            this.quickBaseService=quickBaseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> InformationThumbnailPartial()
        {

            List<InformationThumbnailServiceModel> serviceModels =
                await quickBaseService.GetStaredInformationPosts();

            List<InformationThumbnailViewModel> informationThumbnailViewModels = serviceModels.Select(serviceModel => new InformationThumbnailViewModel
            {
                Summary = serviceModel.Summary,
                ThumbnailImageLink = serviceModel.ThumbnailImageLink,
                Topic = serviceModel.Topic
            }).ToList();


            return PartialView("PartialViews/InformationThumbnail",informationThumbnailViewModels);
        }
    }
}
