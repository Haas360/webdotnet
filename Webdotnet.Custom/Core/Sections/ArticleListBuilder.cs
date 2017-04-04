using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class ArticleListBuilder : ISectionBuilder
    {
        private readonly INodeHelper _nodeHelper;

        public ArticleListBuilder(INodeHelper nodeHelper)
        {
            _nodeHelper = nodeHelper;
        }
        public string ViewName => "ArticleListView";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            var articlesIdInString = content.GetPropertyValue<string>("articlesPicker");
            var articlesIds = articlesIdInString.Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            var viewModel = new ArticleListViewModel
            {
                Articles = new List<ArticleCardViewModel>(),
                Title = content.GetPropertyValue<string>("header")
            };
            articlesIds.ForEach(articleId =>
            {
                var articleModel = _nodeHelper.GetContent(articleId);
                viewModel.Articles.Add(new ArticleCardViewModel
                {
                    Title = articleModel.GetPropertyValue<string>("title"),
                    ShortDescription = articleModel.GetPropertyValue<string>("shortDescription"),
                    Tags = articleModel.GetPropertyValue<string>("tags").Split(','),
                    CardImage = articleModel.GetImage("cardImage", _nodeHelper).WithQuality(80).WithHeight(160).WithWidth(330).WithCrop(),
                    Url = articleModel.Url
                });
            });
            return viewModel;
        }

        public bool DeosApply(string documentAlias)
        {
            return SectionDocumentTypes.ArticleList == documentAlias;
        }
    }
}
