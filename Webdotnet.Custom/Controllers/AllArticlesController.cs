using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Controllers
{
    public class AllArticlesController : RenderMvcController
    {
        private readonly INodeHelper _nodeHelper;
        private readonly IPageModelExtender _pageModelExtender;

        public AllArticlesController(INodeHelper nodeHelper, IPageModelExtender pageModelExtender)
        {
            _nodeHelper = nodeHelper;
            _pageModelExtender = pageModelExtender;
        }
        public override ActionResult Index(RenderModel model)
        {
            var rootNodes = _nodeHelper.Umbraco.TypedContentAtRoot();
            var articlesRootNode = rootNodes.First(x => x.DocumentTypeAlias == "articlesRoot");
            var allArticles = articlesRootNode.Children.SelectMany(x => x.Children).ToList();

            var parsedArticles = allArticles.Select(x => new MetadataForAllList
            {
                Title = x.Name,
                Url = x.Url,
                Date = x.CreateDate 
            }).ToList();
            var grupedArticlesList = new List<GrupedArticles>();
            var index = 0;
            parsedArticles.ForEach(article =>
            {
                var parsedDate = article.Date.ToString("Y", CultureInfo.GetCultureInfoByIetfLanguageTag("pl")).ToFirstUpper();
                if (grupedArticlesList.Any(x => x.NodeName == parsedDate))
                {
                    var monthList = grupedArticlesList.First(x => x.NodeName == parsedDate);
                    monthList.Articles.Add(article);
                }
                else
                {
                    grupedArticlesList.Add(new GrupedArticles
                    {
                        Order = index,
                        NodeName = parsedDate,
                        Articles = new List<MetadataForAllList> { article }
                    });
                    index++;
                }
            });

            var pageViewModel = _pageModelExtender.ApplyLayoutToModel(new PageViewModel(), model.Content);

            grupedArticlesList.ForEach(x => { x.Articles = x.Articles.OrderByDescending(y => y.Date).ToList(); });

            var allArticlesViweModel = new AllArticlesViewModel
            {
                Header = pageViewModel.Header,
                Description = "Wszystkie posty pogrupowane po miesiącu publikacji",
                Title = "Wszystkie posty",
                Footer = pageViewModel.Footer,
                IsArticle = false,
                Id = pageViewModel.Id,
                GrupedArticles = grupedArticlesList.OrderByDescending(x=>x.Order).ToList()
            };

            return View("AllArticles", allArticlesViweModel);
        }

    }

    public class AllArticlesViewModel : PageViewModel
    {
        public List<GrupedArticles> GrupedArticles { get; set; }
    }
    public class GrupedArticles
    {
        public string NodeName { get; set; }
        public int Order { get; set; }
        public List<MetadataForAllList> Articles { get; set; }
    }
    public class MetadataForAllList
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
    }
}
