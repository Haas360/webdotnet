using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class FooterBuilder : ISectionBuilder
    {
        public string ViewName => "FooterView";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            return new FooterVIewModel
            {
                TestMessage = content.GetPropertyValue<string>("testMessage")
            };
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == DocumentTypes.Footer;
        }
    }
}
