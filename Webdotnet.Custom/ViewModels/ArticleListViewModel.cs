using System.Collections.Generic;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;

namespace Webdotnet.Custom.ViewModels
{
    public class ArticleListViewModel: BaseViewModel
    {
        public IList<ArticleCardViewModel> Articles { get; set; }
        public string Title { get; set; }
    }

    public class ArticleCardViewModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public IList<string> Tags { get; set; }
        public ImageWraper CardImage { get; set; }
    }
}
