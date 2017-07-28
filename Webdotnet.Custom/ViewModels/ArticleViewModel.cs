using System;
using System.Collections.Generic;
using Webdotnet.Custom.Core.Helpers;

namespace Webdotnet.Custom.ViewModels
{
    public class ArticleViewModel : PageViewModel
    {
        public ImageWraper Image { get; set; }
        public List<ArticleRecentElement> RecentArticles { get; set; }
        public IEnumerable<TagWithLink> Tags { get; set; }
        public DateTime PublishedOn { get; set; }
    }
}
