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
    public class ArticleController : RenderMvcController
    {
        private readonly IPageModelExtender _pageModelExtender;
        private readonly INodeHelper _nodeHelper;
        private readonly ISectionsProvider _sectionsProvider;

        public ArticleController(ISectionsProvider sectionsProvider, IPageModelExtender pageModelExtender, INodeHelper nodeHelper)
        {
            _pageModelExtender = pageModelExtender;
            _nodeHelper = nodeHelper;
            _sectionsProvider = sectionsProvider;
        }
        [DonutOutputCache(CacheProfile = "Page.Cache")]
        public override ActionResult Index(RenderModel model)
        {
            var allSections = model.Content.Children.ToList();
            var listOfSectionsToRender = _sectionsProvider.GetListOfSectionsToRender(allSections);
            var pageViewModel = new PageViewModel { Sections = listOfSectionsToRender };
            pageViewModel = _pageModelExtender.ApplyLayoutToModel(pageViewModel, model.Content);
            var rootNodes = _nodeHelper.Umbraco.TypedContentAtRoot();
            var websiteNode = rootNodes.First(x => x.DocumentTypeAlias == "master");
            var articleViewModel = pageViewModel.ExtendToArticleViewModel(model.Content, websiteNode);
            var image = model.Content.GetImage("cardImage", _nodeHelper).WithQuality(75).WithHeight(160).WithWidth(330).WithCrop();
            var request = HttpContext.Request;
            var domainurl = string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority);
            articleViewModel.ArticleImgUrl = domainurl +image.Url;

            articleViewModel.Image = model.Content.GetImage("cardImage", _nodeHelper).WithQuality(75).WithHeight(400).WithWidth(1170).WithCrop();
            articleViewModel.PublishedOn = model.Content.CreateDate;
            return View("Article", articleViewModel);
        }
    }
}
