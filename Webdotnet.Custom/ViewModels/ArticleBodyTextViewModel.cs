using System.Web;
using Webdotnet.Custom.Core.SectionBuilder;

namespace Webdotnet.Custom.ViewModels
{
    public class ArticleBodyTextViewModel: BaseViewModel
    {
        public IHtmlString Body { get; set; }
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
 