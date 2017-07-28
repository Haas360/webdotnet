using System;
using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class YouTubeIFrameBuilder: ISectionBuilder
    {
        public string ViewName => "YouTubeIFrame";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            return new YouTubeIFrameViewModel
            {
                Url = content.GetPropertyValue<string>("link")
            };
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == "youtubeIFrame";
        }
    }
}
