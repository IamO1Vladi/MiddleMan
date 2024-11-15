using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiddleMan.Common.Utilities;
using MiddleManServices.ApiServices.QuickBase.Interfaces;

namespace MiddleMan.Controllers
{
    public class SiteMapController : Controller
    {

        private readonly IQuickBaseService quickBaseService;
      

        public SiteMapController(IQuickBaseService service)
        {
            this.quickBaseService=service;
         
        }

        [Route("sitemap.xml")]
        public async Task<IActionResult> Sitemap()
        {

            string siteMapXml = await quickBaseService.GetDynamicSiteMap();

            return Content(siteMapXml, "application/xml", Encoding.UTF8);
        }
    }
}
