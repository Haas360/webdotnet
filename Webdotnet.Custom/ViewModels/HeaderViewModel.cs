using System.Collections.Generic;
using Webdotnet.Custom.Core.SectionBuilder;

namespace Webdotnet.Custom.ViewModels
{
    public class HeaderViewModel: BaseViewModel
    {
        public List<NavSocials> NavSocials { get; set; }
        public List<NavElement> NavElement { get; set; }
    }
}
