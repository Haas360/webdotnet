using System.Linq;
using System.Web.Mvc;
using DevTrends.MvcDonutCaching;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Webdotnet.Custom.Core;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Controllers
{
    public class PageController : RenderMvcController
    {
        private readonly IPageModelExtender _pageModelExtender;
        private readonly ISectionsProvider _sectionsProvider;

        public PageController(ISectionsProvider sectionsProvider, IPageModelExtender pageModelExtender)
        {
            _pageModelExtender = pageModelExtender;
            _sectionsProvider = sectionsProvider;
        }
        [DonutOutputCache(CacheProfile = "Page.Cache")]
        public override ActionResult Index(RenderModel model)
        {
            var allSections = model.Content.Children.ToList();
            var listOfSectionsToRender = _sectionsProvider.GetListOfSectionsToRender(allSections);
            var pageViewModel = new PageViewModel {Sections = listOfSectionsToRender};
            return View("Page", _pageModelExtender.ApplyLayoutToModel(pageViewModel, model.Content));
        }
    }
}
