using System.Collections.Generic;
using Umbraco.Core.Models;
using Webdotnet.Custom.Core.SectionBuilder;

namespace Webdotnet.Custom.ViewModels
{
    public class CarouselViewModel : BaseViewModel
    {
        public List<Slide> Slides { get; set; }
    }

    public class Slide
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public IPublishedContent Image { get; set; }
    }
}
