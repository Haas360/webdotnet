namespace Webdotnet.Custom.ViewModels
{
    public class LayoutBaseViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public PageSection Header { get; set; }
        public PageSection Footer { get; set; }
        public bool IsArticle { get; set; }
        public string ArticleImgUrl { get; set; }
    }
}
