using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Helpers
{
    public static class ViewModelExtensions
    {
        public static ArticleViewModel ExtendToArticleViewModel(this PageViewModel pageViewModel)
        {
            return new ArticleViewModel
            {
                Header = pageViewModel.Header,
                Title = pageViewModel.Title,
                Footer = pageViewModel.Footer,
                Sections = pageViewModel.Sections,
            };
        }
    }
}
