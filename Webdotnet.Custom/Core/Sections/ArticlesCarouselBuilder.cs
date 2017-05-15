using System;
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
    public class ArticlesCarouselBuilder : ISectionBuilder
    {
        private readonly INodeHelper _nodeHelper;

        public ArticlesCarouselBuilder(INodeHelper nodeHelper)
        {
            _nodeHelper = nodeHelper;
        }
        public string ViewName => "CarouselView";

        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            var slides = new List<Slide>();

            var articlesIds = content.GetPropertyValue<string>("articlesPicker").Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            var articles = _nodeHelper.Umbraco.TypedContent(articlesIds).ToList();
            articles.ForEach(slide =>
            {
                slides.Add(new Slide()
                {
                    Description = slide.GetPropertyValue<string>("shortDescription"),
                    Header = slide.GetPropertyValue<string>("title"),
                    Url = slide.Url,
                    Image = slide.GetImage("cardImage", _nodeHelper).WithHeight(450).WithCrop().WithWidth(1170)
                });
            });

            return new CarouselViewModel()
            {
                Slides = slides
            };
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == "articleCarousel";
        }
    }
}
