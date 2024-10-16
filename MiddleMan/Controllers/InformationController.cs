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

        public IActionResult SinglePost()
        {
            return View("SingleInformationPost");
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

        public async Task<IActionResult> InformationThumbnailListViewPartial(bool stared = false,
            string? category = null, string? recordId = null,int page=1,int perPage=9)
        {
            List<InformationThumbnailServiceModel> serviceModels =
                await quickBaseService.GetInformationPostsBasedOnFilters(stared, category, recordId,page, perPage);

            List<InformationThumbnailViewModel> informationThumbnailViewModels = serviceModels.Select(serviceModel =>
                new InformationThumbnailViewModel
                {
                    Summary = serviceModel.Summary,
                    ThumbnailImageLink = serviceModel.ThumbnailImageLink,
                    Topic = serviceModel.Topic
                }).ToList();

            ViewBag.NumberOfRecords = serviceModels.FirstOrDefault()!.Metadata.TotalRecords;
            ViewBag.CurrentPage = page;

            return PartialView("PartialViews/InformationThumbnailListView", informationThumbnailViewModels);
        }
    }
}
