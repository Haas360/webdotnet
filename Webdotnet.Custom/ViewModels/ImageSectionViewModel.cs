using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Webdotnet.Custom.Core.SectionBuilder;

namespace Webdotnet.Custom.ViewModels
{
    public class ImageSectionViewModel: BaseViewModel
    {
        public string Header { get; set; }
        public IPublishedContent Image { get; set; }
    }
}
