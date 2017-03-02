using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.Sections;
using Webdotnet.Custom.ViewModels;
using Webdotnet.Tests.TestHeplers;

namespace Webdotnet.Tests.SectionBuildersSpecs
{
    [TestFixture]
    public class ImageSectionBuilderTests
    {
        private ImageSectionBuilder _builder;
        private static int _imageId = 5;
        [SetUp]
        public void Setup()
        {
            var nodeHelperMock = new Mock<INodeHelper>();
            nodeHelperMock.Setup(c => c.GetMedia(_imageId)).Returns(new Mock<IPublishedContent>().Object);
            _builder = new ImageSectionBuilder(nodeHelperMock.Object);
        }
        [Test]
        public void BuilderApplyOnlyToHeaderDocType()
        {
            Assert.True(_builder.DeosApply(DocumentTypes.Image));
        }
        [Test]
        public void WhenCreateViewModelItShouldGetTesMessageFromUmbraco()
        {
            var mockedUmbracoContent = new PublishedContentMockingHelper();
            var testMessage = "test header";
            mockedUmbracoContent.AddProperty("header", testMessage);

            var viewModel = (ImageSectionViewModel)_builder.CreateViewModel(mockedUmbracoContent.ContentMock);

            Assert.AreEqual(testMessage, viewModel.Header);
        }
    }
}
