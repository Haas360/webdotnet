using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;

namespace Webdotnet.Custom.ViewModels
{
    public class ImageSectionViewModel: BaseViewModel
    {
        public string Header { get; set; }
        public ImageWraper Image { get; set; }
    }
}
