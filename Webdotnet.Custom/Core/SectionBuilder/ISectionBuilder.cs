using Umbraco.Core.Models;

namespace Webdotnet.Custom.Core.SectionBuilder
{
    public interface ISectionBuilder
    {
        string ViewName { get; }
        BaseViewModel CreateViewModel(IPublishedContent content);
        bool DeosApply(string documentAlias);
    }
}
