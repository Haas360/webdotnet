using System;
using System.Web;
using Umbraco.Web;

namespace Webdotnet.Custom.Core
{
    public class WebdotnetApplication : UmbracoApplication
    {
        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom.Equals("Page.Cache")) 
            {
                return context.Request.Url.AbsoluteUri;
            }
            return base.GetVaryByCustomString(context, custom);
        }
    }
}

