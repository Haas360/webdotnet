using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class HeadlineBuilder : ISectionBuilder
    {
        public string ViewName => "HeadlineView";

        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            return new HeadlineViewModel
            {
                Header = content.GetPropertyValue<string>("header"),
                Body = new MvcHtmlString(content.GetPropertyValue<string>("body"))
            };
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == DocumentTypes.Headline;
        }
    }
}
