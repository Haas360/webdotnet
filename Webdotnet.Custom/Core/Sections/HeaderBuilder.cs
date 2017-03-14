using System.Linq;
using Archetype.Models;
using Newtonsoft.Json;
using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class HeaderBuilder : ISectionBuilder
    {
        public string ViewName => "HeaderView";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            var websiteNode = content.AncestorOrSelf(1);
            var pages = websiteNode.Children.Where(x => x.DocumentTypeAlias == Consts.PageSection);
            var linkList = pages.Select(x => new NavElement()
            {
                IsVisibleInMenu = x.GetPropertyValue<bool>("isVisibleInMenu"),
                Name = x.Name,
                Url = x.Url,
                IsActive = false
            });
            var socialItemsNode = websiteNode.GetPropertyValue<ArchetypeModel>("items");
            var socialItems = socialItemsNode.Select(x=>
            new NavSocials()
            {
                Name = x.GetValue<string>("name"),
                FontAwesomeClass = x.GetValue<string>("fontAwesomeClass"),
                Url = x.GetValue<string>("url"),
            });

            return new HeaderViewModel
            {
                NavElement = linkList.ToList(),
                NavSocials = socialItems.ToList()
            };
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == SectionDocumentTypes.Header;
        }
    }
}
