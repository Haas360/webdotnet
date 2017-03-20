using Umbraco.Core.Models;
using Umbraco.Web;

namespace Webdotnet.Custom.Core.Helpers
{
    public class ImageWraper
    {
        public string Url;
        public string Alias;

        public ImageWraper(string url,string alias)
        {
            Url = url;
            Alias = alias;
        }
    }

    public static class ImageExtension
    {
        public static ImageWraper GetImage(this IPublishedContent model, string imagePropertyAlias, INodeHelper umbracoHelper)
        {
            var mediaContentId = model.GetPropertyValue<int>(imagePropertyAlias);
            var image = umbracoHelper.GetMedia(mediaContentId);
            var imageAlias = image.GetPropertyValue<string>("imageAlias");
            return new ImageWraper(image.Url, imageAlias);
        }

        public static ImageWraper WithQuality(this ImageWraper wraper, int quality)
        {
            return AppendParameterToUrl(wraper, "quality=" + quality);
        }
        public static ImageWraper WithHeight(this ImageWraper wraper, int height)
        {
            return AppendParameterToUrl(wraper, "height=" + height);
        }
        public static ImageWraper WithWidth(this ImageWraper wraper, int width)
        {
            return AppendParameterToUrl(wraper, "width=" + width);
        }
        public static ImageWraper WithWidthRatio(this ImageWraper wraper, double widthratio)
        {
            return AppendParameterToUrl(wraper, "widthratio=" + widthratio);
        }
        public static ImageWraper WithCrop(this ImageWraper wraper)
        {
            return AppendParameterToUrl(wraper, "mode=crop");
        }
        public static ImageWraper WithCustomAlias(this ImageWraper wraper, string alias)
        {
            wraper.Alias = alias;
            return wraper;
        }
        private static ImageWraper AppendParameterToUrl(ImageWraper wraper, string parameter)
        {
            var urlParamsSpliter = wraper.Url.Contains("?") ? "&" : "?";
            wraper.Url = wraper.Url + urlParamsSpliter + parameter;
            return wraper;
        }
    }
}
