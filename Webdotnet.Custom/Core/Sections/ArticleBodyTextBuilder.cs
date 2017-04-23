using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class ArticleBodyTextBuilder : ISectionBuilder
    {
        public string ViewName => "ArticleBodyText";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            return new ArticleBodyTextViewModel()
            {
                Id = content.Id,
                Body = new MvcHtmlString(content.GetPropertyValue<string>("body"))
            };
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == "articleText";
        }
    }
}
