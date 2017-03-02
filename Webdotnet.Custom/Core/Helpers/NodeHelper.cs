using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Webdotnet.Custom.Core.Helpers
{
    public interface INodeHelper
    {
        UmbracoHelper Umbraco { get; }
        IPublishedContent GetMedia(int imageId);
        IPublishedContent GetContent(int imageId);
    }
    
    public class NodeHelper : INodeHelper
    {
        private static UmbracoHelper _umbracoHelper;

        public UmbracoHelper Umbraco => _umbracoHelper ?? (_umbracoHelper = new UmbracoHelper(UmbracoContext.Current));

        public IPublishedContent GetMedia(int imageId) => Umbraco.TypedMedia(imageId);
        public IPublishedContent GetContent(int imageId) => Umbraco.TypedContent(imageId);
    }
}
