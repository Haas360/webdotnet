﻿using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class ImageSectionBuilder : ISectionBuilder
    {
        private readonly INodeHelper _nodeHelper;
        public string ViewName => "ImageSectionView";

        public ImageSectionBuilder(INodeHelper nodeHelper)
        {
            _nodeHelper = nodeHelper;
        }
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            return new ImageSectionViewModel
            {
                Header = content.GetPropertyValue<string>("header"),
                Image = _nodeHelper.GetMedia(content.GetPropertyValue<int>("image"))
            };
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == DocumentTypes.Image;
        }
    }
}
