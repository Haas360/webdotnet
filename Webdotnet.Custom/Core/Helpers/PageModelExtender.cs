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
        private readonly IBuildersFactory _buildersFactory;

        public PageModelExtender(INodeHelper nodeHelper, IBuildersFactory buildersFactory)
        {
            _nodeHelper = nodeHelper;
            _buildersFactory = buildersFactory;
        }
        public PageViewModel ApplyLayoutToModel(PageViewModel viewModel, IPublishedContent model)
        {
            var rootNodes = _nodeHelper.Umbraco.TypedContentAtRoot();
            var websiteNode = rootNodes.First(x => x.DocumentTypeAlias == "master");
            var header = BuildSection(websiteNode, SectionDocumentTypes.Header);
            var footer = BuildSection(websiteNode, SectionDocumentTypes.Footer);
            var customTitle = model.GetPropertyValue<string>("title");

            viewModel.Title = string.IsNullOrEmpty(customTitle) ? websiteNode.GetPropertyValue<string>("title") : customTitle;
            
            var customDesc = model.GetPropertyValue<string>("description");

            viewModel.Description = customDesc;
            viewModel.Header = header;
            viewModel.Footer = footer;
            viewModel.Id = model.Id;
            return viewModel;
        }
       

        private PageSection BuildSection(IPublishedContent websiteNode,  string nodeAlias)
        {
            var node = _nodeHelper.GetContent(websiteNode.GetPropertyValue<int>(nodeAlias));
            var builder = _buildersFactory.GetFirstBuilderThatApply(node.DocumentTypeAlias);
            return builder != null ? new PageSection
            {
                PartialPath = builder.ViewName,
                ViewModel = builder.CreateViewModel(node)
            }
            : null;
        }
    }
}
