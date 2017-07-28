using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class CodeSnippetBuilder : ISectionBuilder
    {
        public string ViewName => "CodeSnippet";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            var viewModel = new CodeSnippetViewModel
            {
                Language = content.GetPropertyValue<string>("languagePicker"),
                Code = content.GetPropertyValue<string>("code")
            };
            return viewModel;
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == "codeSnippet";
        } 
    }
}
