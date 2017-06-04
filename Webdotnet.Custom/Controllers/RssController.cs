using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Webdotnet.Custom.Controllers
{
    public class RssController : RenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {
            var request = WebRequest.Create("http://google.pl");//new HttpWebRequest();
            WebResponse response = request.GetResponse();


            // Get the stream containing all content returned by the requested server.
            var dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            string responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            //            var ss = request.BeginGetResponse(ar => ar.)
            return base.Index(model);
        }
    }
}
