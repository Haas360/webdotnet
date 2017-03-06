using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class HeaderBuilder : ISectionBuilder
    {
        public string ViewName => "HeaderView";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            return new HeaderViewModel
            {
                TestString = content.GetPropertyValue<string>("testMessage")
            };
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == DocumentTypes.Header;
        }
    }
}
