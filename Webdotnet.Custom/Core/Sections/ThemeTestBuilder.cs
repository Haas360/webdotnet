using Umbraco.Core.Models;
using Webdotnet.Custom.Core.SectionBuilder;

namespace Webdotnet.Custom.Core.Sections
{
    public class ThemeTestBuilder: ISectionBuilder
    {
        public string ViewName => "ThemeTest";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            return new BaseViewModel();
        }

        public bool DeosApply(string documentAlias)
        {
           return documentAlias == "themeTest";
        }
    }
}
