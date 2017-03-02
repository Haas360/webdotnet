using NUnit.Framework;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.Sections;
using Webdotnet.Custom.ViewModels;
using Webdotnet.Tests.TestHeplers;

namespace Webdotnet.Tests.SectionBuildersSpecs
{
    [TestFixture]
    public class FooterBuilderTests
    {
        private readonly FooterBuilder _builder = new FooterBuilder();
        [Test]
        public void BuilderApplyOnlyToHeaderDocType()
        {
            Assert.True(_builder.DeosApply(DocumentTypes.Footer));
        }
        [Test]
        public void WhenCreateViewModelItShouldGetTestMessageFromUmbraco()
        {
            var mockedUmbracoContent = new PublishedContentMockingHelper();
            var testMessage = "test message 1";
            mockedUmbracoContent.AddProperty("testMessage", testMessage);

            var viewModel = (FooterVIewModel)_builder.CreateViewModel(mockedUmbracoContent.ContentMock);

            Assert.AreEqual(testMessage, viewModel.TestMessage);
        }

    }
}
