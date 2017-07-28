using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Helpers
{
    public static class ViewModelExtensions
    {
        public static ArticleViewModel ExtendToArticleViewModel(this PageViewModel pageViewModel, IPublishedContent modelContent, IPublishedContent websiteNode)
        {
            var title = modelContent.GetPropertyValue<string>("title");
            var description = modelContent.GetPropertyValue<string>("shortDescription");
            var recentArticles = modelContent.Parent.Children
              .Where(x => x.Id != modelContent.Id)
              .OrderByDescending(x => x.CreateDate)
              .Take(7)
              .Select(x => new ArticleRecentElement
              {
                  Title = x.GetPropertyValue<string>("title"),
                  Url = x.Url
              }).ToList();
            var articlesByTagViewer = websiteNode.Children.First(x=>x.DocumentTypeAlias == "Tag");
            var tags = modelContent.GetPropertyValue<string>("tags").Split(',');
            var tagsWithLinks = tags.Select(x => new TagWithLink
            {
                Title = x,
                Url = articlesByTagViewer.Url + $"?tag={x}"
            });
            
            return new ArticleViewModel
            {
                Id = modelContent.Id,
                Header = pageViewModel.Header,
                Title = title,
                RecentArticles = recentArticles,
                Description = description,
                Footer = pageViewModel.Footer,
                Sections = pageViewModel.Sections,
                Tags = tagsWithLinks,
                IsArticle = true
            };
        }

    }
    public class ArticleRecentElement
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class TagWithLink
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
