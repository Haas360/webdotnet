using NUnit.Framework;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.Sections;
using Webdotnet.Custom.ViewModels;
using Webdotnet.Tests.TestHeplers;

namespace Webdotnet.Tests.SectionBuildersSpecs
{
    public class HeadlineBuilderTests
    {
        private readonly HeadlineBuilder _builder = new HeadlineBuilder();
        private readonly string _testTitle = "test title";
        private readonly string _testBody = "<p>test body</p>";
        [Test]
        public void BuilderApplyOnlyToHeaderDocType()
        {
            Assert.True(_builder.DeosApply(DocumentTypes.Headline));
        }
        [Test]
        public void WhenCreateViewModelItShouldGetTitleFromUmbraco()
        {
            var mockedViewModel = CreateMockContent();
            Assert.AreEqual(_testTitle, mockedViewModel.Header);
        }
        [Test]
        public void WhenCreateViewModelItShouldGetBodyFromUmbraco()
        {
            var mockedViewModel = CreateMockContent();
            Assert.AreEqual(_testBody, mockedViewModel.Body.ToHtmlString());
        }
        private HeadlineViewModel CreateMockContent()
        {
            var mockedUmbracoContent = new PublishedContentMockingHelper();
            mockedUmbracoContent.AddProperty("header", _testTitle);
            mockedUmbracoContent.AddProperty("body", _testBody);
            return (HeadlineViewModel)_builder.CreateViewModel(mockedUmbracoContent.ContentMock);
        }
    }
}
