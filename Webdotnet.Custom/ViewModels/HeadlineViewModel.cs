using System.Web;
using Webdotnet.Custom.Core.SectionBuilder;

namespace Webdotnet.Custom.ViewModels
{
    public class HeadlineViewModel : BaseViewModel
    {
        public string Header { get; set; }
        public IHtmlString Body { get; set; }
    }
}
