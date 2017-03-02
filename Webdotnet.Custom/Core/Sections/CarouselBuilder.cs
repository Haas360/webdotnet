using System;
using Umbraco.Core.Models;
using Webdotnet.Custom.Core.SectionBuilder;

namespace Webdotnet.Custom.Core.Sections
{
    public class CarouselBuilder : ISectionBuilder
    {
        public string ViewName => "nothing";

        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            throw new NotImplementedException();
        }

        public bool DeosApply(string documentAlias)
        {
            return false;
        }
    }
}
