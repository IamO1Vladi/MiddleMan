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
                Topic = serviceModel.Topic,
                PostImages = serviceModel.PostImages
            };

            ViewData["CurrentUrl"] = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";

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
                await quickBaseService.GetInformationPostsBasedOnFilters(stared, category,page, perPage);

            if (!serviceModels.Any())
            {
                return StatusCode(204);
            }

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
            ViewBag.CurrentCategory= category;

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

        public async Task<IActionResult> NotFoundPageInformationPostsPartialView(string keyword,int postsToReturn)
        {
            List<InformationThumbnailServiceModel> serviceModels =
                await quickBaseService.GetInformationPostsBasedOnAKeyword(keyword,postsToReturn);

            if (!serviceModels.Any())
            {
                return StatusCode(204);
            }

            List<InformationThumbnailViewModel> informationThumbnailViewModels = serviceModels.Select(serviceModel =>
                new InformationThumbnailViewModel
                {
                    Summary = serviceModel.Summary,
                    ThumbnailImageLink = serviceModel.ThumbnailImageLink,
                    Topic = serviceModel.Topic,
                    RecordId = serviceModel.RecordId
                }).ToList();

            return PartialView("PartialViews/NotFoundInformationPostList", informationThumbnailViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> SubscribeToNewsLetter([FromBody] SubscribeToNewsLetterServiceModel formInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await quickBaseService.SubscribeCustomerToNewsLetter(formInfo);
                    return Json(new { success = true, message = "Thank you for subscribing to our newsletter!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "There was an error subscribing to our newsletter. Please try again later." });
                }
            }

            return Json(new { success = false, message = "Invalid data. Please check your inputs and try again." });
        }
    }
}
