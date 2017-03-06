using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core.Helpers
{
    public interface IPageModelExtender
    {
        PageViewModel ApplyLayoutToModel(PageViewModel viewModel, IPublishedContent model);
    }

    public class PageModelExtender : IPageModelExtender
    {
        private readonly INodeHelper _nodeHelper;
        private readonly IList<ISectionBuilder> _sectionBuilders;
        
        public PageModelExtender(INodeHelper nodeHelper, IList<ISectionBuilder> sectionBuilders)
        {
            //TODO: change this to factory also
            _nodeHelper = nodeHelper;
            _sectionBuilders = sectionBuilders;
        }
        public PageViewModel ApplyLayoutToModel(PageViewModel viewModel, IPublishedContent model)
        {
            var websiteNode = model.AncestorOrSelf(1);

            var header = BuildSection(websiteNode, _sectionBuilders, DocumentTypes.Header);
            var footer = BuildSection(websiteNode, _sectionBuilders, DocumentTypes.Footer);

            viewModel.Title = websiteNode.GetPropertyValue<string>("title");
            viewModel.Header = header;
            viewModel.Footer = footer;

            return viewModel;
        }

        private PageSection BuildSection(IPublishedContent websiteNode, IList<ISectionBuilder> sectionBuilders, string nodeAlias)
        {
            var node = _nodeHelper.GetContent(websiteNode.GetPropertyValue<int>(nodeAlias));
            var builder = sectionBuilders.FirstOrDefault(x => x.DeosApply(node.DocumentTypeAlias));
            return builder != null ? new PageSection
            {
                PartialPath = builder.ViewName,
                ViewModel = builder.CreateViewModel(node)
            }
            : null;
        }
    }
}
