using System.Collections.Generic;
using Webdotnet.Custom.Core.SectionBuilder;

namespace Webdotnet.Custom.ViewModels
{
    public class PageViewModel : LayoutBaseViewModel
    {
        public IList<PageSection> Sections { get; set; }
        public int Id { get; set; }
    }

    public class PageSection
    {
        public string PartialPath { get; set; }
        public BaseViewModel ViewModel { get; set; }
    }
}
