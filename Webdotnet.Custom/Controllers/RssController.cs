using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevTrends.MvcDonutCaching;
using HtmlAgilityPack;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Webdotnet.Custom.Core.Helpers;

namespace Webdotnet.Custom.Controllers
{
    public class RssController : RenderMvcController
    {
        private readonly INodeHelper _nodeHelper;

        public RssController(INodeHelper nodeHelper)
        {
            _nodeHelper = nodeHelper;
        }
        [DonutOutputCache(CacheProfile = "Page.Cache")]
        public override ActionResult Index(RenderModel model)
        {
            var tagName = Request.QueryString["tag"];
            List<IPublishedContent> articles;

            if (!string.IsNullOrEmpty(tagName))
            {
                articles = _nodeHelper.Umbraco.TagQuery.GetContentByTag(tagName).ToList();
            }
            else
            {
                var rootNodes = _nodeHelper.Umbraco.TypedContentAtRoot();
                var articlesRootNode = rootNodes.First(x => x.DocumentTypeAlias == "articlesRoot");
                articles = articlesRootNode.Children.SelectMany(x => x.Children).ToList();
            }
            var listOfRss = new List<RssModel>();

            articles.OrderByDescending(x => x.CreateDate)
                .ToList()
                .ForEach(article =>
                    {
                        var request = WebRequest.Create(article.UrlAbsolute());//new HttpWebRequest();
                        WebResponse response = request.GetResponse();

                        var dataStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(responseFromServer);

                        var description = doc.DocumentNode.SelectSingleNode("//*[@id='content']").InnerText;
                        // Clean up the streams.
                        listOfRss.Add(new RssModel
                        {
                            Url = article.UrlAbsolute(),
                            Title = article.Name,
                            Description = description,
                            Guid = article.GetKey().ToString()
                        });
                        reader.Close();
                        dataStream.Close();
                        response.Close();
                    });

            return View("Rss", listOfRss);
        }
    }

    public class RssModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Guid { get; set; }
        
    }
}
