using System.Collections.Generic;
using Webdotnet.Custom.Core.Helpers;
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
        public ImageWraper Image { get; set; }
    }
}
