using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevTrends.MvcDonutCaching;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Controllers
{
    public class TagController : RenderMvcController
    {
        private readonly IPageModelExtender _pageModelExtender;
        private readonly INodeHelper _nodeHelper;
        private readonly IBuildersFactory _buildersFactory;

        public TagController(IPageModelExtender pageModelExtender, INodeHelper nodeHelper, IBuildersFactory buildersFactory)
        {
            _pageModelExtender = pageModelExtender;
            _nodeHelper = nodeHelper;
            _buildersFactory = buildersFactory;
        }
        [DonutOutputCache(CacheProfile = "Page.Cache")]
        public override ActionResult Index(RenderModel model)
        {
            var pageViewModel = new PageViewModel();
            var extendedModel = _pageModelExtender.ApplyLayoutToModel(pageViewModel, model.Content);
            var tagName = Request.QueryString["tag"];
            var articles = new List<ArticleCardViewModel>();
            var articlesWithTag = _nodeHelper.Umbraco.TagQuery.GetContentByTag(tagName).ToList();
            var header = $"Posty otagowane {tagName}";
            articlesWithTag.ForEach(article =>
            {
                articles.Add(new ArticleCardViewModel
                {
                    Title = article.GetPropertyValue<string>("title"),
                    ShortDescription = article.GetPropertyValue<string>("shortDescription"),
                    Tags = article.GetPropertyValue<string>("tags").Split(','),
                    CardImage = article.GetImage("cardImage", _nodeHelper).WithQuality(80).WithHeight(160).WithWidth(330).WithCrop(),
                    Url = article.Url
                });
            });
            var modelForArticles = new ArticleListViewModel
            {
                Title = header,
                Articles = articles
            };
            var viewModel = new ArticleByTagViewModel
            {
                Id = extendedModel.Id,
                Title = header,
                Footer = extendedModel.Footer,
                Header = extendedModel.Header,
                Description = "Lista wszystkich postów które zosta³y otagowane " + tagName,
                ArticlesListModel = modelForArticles
            };
            return View("Tag", viewModel);
        }
    }
}
