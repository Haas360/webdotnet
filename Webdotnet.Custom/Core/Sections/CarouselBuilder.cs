using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class CarouselBuilder : ISectionBuilder
    {
        private readonly INodeHelper _nodeHelper;

        public CarouselBuilder(INodeHelper nodeHelper)
        {
            _nodeHelper = nodeHelper;
        }
        public string ViewName => "CarouselView";

        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            var slides = new List<Slide>();

            content.Children.ForEach(slide =>
            {
                slides.Add(new Slide()
                {
                    Description = slide.GetPropertyValue<string>("shortDescription"),
                    Header = slide.GetPropertyValue<string>("header"),
                    Url = slide.GetPropertyValue<string>("slideUrl"),
                    Image = slide.GetImage("image", _nodeHelper).WithHeight(450).WithCrop().WithWidth(1170)
                });
            });

            return new CarouselViewModel()
            {
                Slides = slides
            };
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == "carousel";
        }
    }
}
