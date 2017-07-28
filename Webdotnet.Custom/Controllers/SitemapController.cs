using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DevTrends.MvcDonutCaching;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Webdotnet.Custom.Core.Helpers;

namespace Webdotnet.Custom.Controllers
{
    [DonutOutputCache(CacheProfile = "Page.Cache")]
    public class SitemapController : RenderMvcController
    {
        private readonly INodeHelper _nodeHelper;

        public SitemapController(INodeHelper nodeHelper)
        {
            _nodeHelper = nodeHelper;
        }

        public override ActionResult Index(RenderModel model)
        {
            var rootNodes = _nodeHelper.Umbraco.TypedContentAtRoot().ToList();
            var websiteNode = rootNodes.First(x => x.DocumentTypeAlias == "master");
            var articlesRootNode = rootNodes.First(x => x.DocumentTypeAlias == "articlesRoot");

            var listOfPagesForSitemap = new List<SitemapPage>();

            websiteNode.Children.ForEach(page =>
            {
                if (page.GetPropertyValue<bool>("isInSiteMap") && page.DocumentTypeAlias == "page")
                {
                    listOfPagesForSitemap.Add(new SitemapPage
                    {
                        Url = page.UrlAbsolute(),
                        LastModifiedDateString = page.UpdateDate.ToString("yyyy-MM-dd")
                    });
                }
            });

            var allArticles = articlesRootNode.Children.SelectMany(x => x.Children).ToList();
            allArticles.ForEach(article =>
            {
                listOfPagesForSitemap.Add(new SitemapPage
                {
                    Url = article.UrlAbsolute(),
                    LastModifiedDateString = article.UpdateDate.ToString("yyyy-MM-dd")
                });
            });

            return View("Sitemap", listOfPagesForSitemap);
        }
    }

    public class SitemapPage
    {
        public string Url { get; set; }
        public string LastModifiedDateString { get; set; }

    }
}
