using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;
using Umbraco.Web;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.Sections;
using Webdotnet.Custom.ViewModels;
using Webdotnet.Tests.TestHeplers;

namespace Webdotnet.Tests.SectionBuildersSpecs
{
    [TestFixture]
    public class HeaderBuilderTests
    {
        private readonly HeaderBuilder _builder = new HeaderBuilder();
        [Test]
        public void BuilderApplyOnlyToHeaderDocType()
        {
            Assert.True(_builder.DeosApply(DocumentTypes.Header));
        }
        [Test]
        public void WhenCreateViewModelItShouldGetTesMessageFromUmbraco()
        {
            var mockedUmbracoContent = new PublishedContentMockingHelper();
            var testMessage = "test message 1";
            mockedUmbracoContent.AddProperty("testMessage", testMessage);
            
            var viewModel = (HeaderViewModel)_builder.CreateViewModel(mockedUmbracoContent.ContentMock);

            Assert.AreEqual(testMessage, viewModel.TestString);
        }
        
    }
}
