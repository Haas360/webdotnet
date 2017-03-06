using Umbraco.Core.Models;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class ErrorSectionBuilder : ISectionBuilder
    {
        public string ViewName => Consts.SectionErrorViewName;
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            var viewModel = new SectionErrorViewModel
            {
                SectionName = content.Name,
                ErrorMsg = "You need to create Section builder for alias: " + content.DocumentTypeAlias
            };
            return viewModel;
        }

        public bool DeosApply(string documentAlias)
        {
            return false;
        }
    }
}
