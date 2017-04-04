using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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

        public override ActionResult Index(RenderModel model)
        {
            var allSections = model.Content.Children.ToList();
            var listOfSectionsToRender = _sectionsProvider.GetListOfSectionsToRender(allSections);
            var pageViewModel = new PageViewModel { Sections = listOfSectionsToRender };
            pageViewModel = _pageModelExtender.ApplyLayoutToModel(pageViewModel, model.Content);
            var articleViewModel = pageViewModel.ExtendToArticleViewModel();
            articleViewModel.Image = model.Content.GetImage("cardImage", _nodeHelper).WithQuality(90).WithHeight(400).WithWidth(1170).WithCrop();
            return View("Article", articleViewModel);
        }
    }
}
