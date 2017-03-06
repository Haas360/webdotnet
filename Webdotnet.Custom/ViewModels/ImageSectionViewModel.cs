﻿using Umbraco.Core.Models;
using Webdotnet.Custom.Core.SectionBuilder;

namespace Webdotnet.Custom.ViewModels
{
    public class ImageSectionViewModel: BaseViewModel
    {
        public string Header { get; set; }
        public IPublishedContent Image { get; set; }
    }
}
