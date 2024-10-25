using Microsoft.AspNetCore.Mvc;
using MiddleMan.Models;
using MiddleMan.Web.ViewModels.InformationSection;
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

        public async Task<IActionResult> SinglePost(string recordId)
        {

            InformationSinglePostServiceModel serviceModel = await quickBaseService.GetInformationSinglePost(recordId);

            await quickBaseService.UpdateSinglePostUserViews(recordId, serviceModel.PostViews);

            InformationSinglePostViewModel singlePostViewModel = new InformationSinglePostViewModel
            {
                Category = serviceModel.Category,
                FirstParagraph = serviceModel.FirstParagraph,
                HeaderImageUrl = serviceModel.HeaderImageUrl,
                SecondParagraph = serviceModel.SecondParagraph,
                SectionImageUrl = serviceModel.SectionImageUrl,
                Topic = serviceModel.Topic
            };

            return View("SingleInformationPost",singlePostViewModel);
        }

        public async Task<IActionResult> InformationThumbnailPartial()
        {

            List<InformationThumbnailServiceModel> serviceModels =
                await quickBaseService.GetStaredInformationPosts();

            List<InformationThumbnailViewModel> informationThumbnailViewModels = serviceModels.Select(serviceModel => new InformationThumbnailViewModel
            {
                Summary = serviceModel.Summary,
                ThumbnailImageLink = serviceModel.ThumbnailImageLink,
                Topic = serviceModel.Topic,
                RecordId = serviceModel.RecordId
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
                    Topic = serviceModel.Topic,
                    RecordId = serviceModel.RecordId
                }).ToList();

            ViewBag.NumberOfRecords = serviceModels.FirstOrDefault()!.Metadata.TotalRecords;
            ViewBag.CurrentPage = page;

            return PartialView("PartialViews/InformationThumbnailListView", informationThumbnailViewModels);
        }

        public async Task<IActionResult> MostViewedInformationPostsPartialView(int numberOfPosts)
        {

            List<InformationThumbnailServiceModel> serviceModels =
                await quickBaseService.GetMostViewedInformationPosts(numberOfPosts);

            List<InformationThumbnailViewModel> informationThumbnailViewModels = serviceModels.Select(serviceModel =>
                new InformationThumbnailViewModel
                {
                    Summary = serviceModel.Summary,
                    ThumbnailImageLink = serviceModel.ThumbnailImageLink,
                    Topic = serviceModel.Topic,
                    RecordId = serviceModel.RecordId
                }).ToList();

            return PartialView("PartialViews/MostViewdPostPartialView", informationThumbnailViewModels);

        }
    }
}
