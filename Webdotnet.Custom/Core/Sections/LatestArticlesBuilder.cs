using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class LatestArticlesBuilder :ISectionBuilder
    {
        private readonly INodeHelper _nodeHelper;

        public LatestArticlesBuilder(INodeHelper nodeHelper)
        {
            _nodeHelper = nodeHelper;
        }
        public string ViewName => "ArticleListView";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            var rootNodes = _nodeHelper.Umbraco.TypedContentAtRoot();
            var articlesRootNode = rootNodes.First(x => x.DocumentTypeAlias == "articlesRoot");
            var allArticles = articlesRootNode.Children.SelectMany(x => x.Children).ToList();

            var viewModel = new ArticleListViewModel
            { 
                Articles = new List<ArticleCardViewModel>(),
                Title = content.GetPropertyValue<string>("header")
            };
            var articlesToDisplay = allArticles.OrderByDescending(x => x.CreateDate).Take(9);
            articlesToDisplay.ForEach(articleModel =>
            {
                viewModel.Articles.Add(new ArticleCardViewModel
                {
                    Title = articleModel.GetPropertyValue<string>("title"),
                    ShortDescription = articleModel.GetPropertyValue<string>("shortDescription"),
                    Tags = articleModel.GetPropertyValue<string>("tags").Split(','),
                    CardImage = articleModel.GetImage("cardImage", _nodeHelper).WithQuality(70).WithHeight(160).WithWidth(330).WithCrop(),
                    Url = articleModel.Url
                });
            });
            return viewModel;
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == "latestArticles";
        }
    }
}
