using System.Linq;
using Archetype.Models;
using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Sections
{
    public class FooterBuilder : ISectionBuilder
    {
        private readonly INodeHelper _nodeHelper;

        public FooterBuilder(INodeHelper nodeHelper)
        {
            _nodeHelper = nodeHelper;
        }
        public string ViewName => "FooterView";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            var websiteNode = content.AncestorOrSelf(1);
            var socialItemsNode = websiteNode.GetPropertyValue<ArchetypeModel>("items");
            var socialItems = socialItemsNode.Select(x =>
            new NavSocials()
            {
                Name = x.GetValue<string>("name"),
                FontAwesomeClass = x.GetValue<string>("fontAwesomeClass"),
                Url = x.GetValue<string>("url"),
            });

            return new FooterViewModel
            {
                NavSocials = socialItems.ToList()
            };
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == SectionDocumentTypes.Footer;
        }
    }
}
