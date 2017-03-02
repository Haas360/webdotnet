using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.Core.Sections;
using Webdotnet.Custom.ViewModels;
using Webdotnet.Tests.TestHeplers;

namespace Webdotnet.Tests
{
    [TestFixture]
    public class PageModelExtenderTests
    {
        private List<ISectionBuilder> _buildersList;
        private PageModelExtender _pageExtender;
        IPublishedContent _websiteNode;
        [SetUp]
        public void SetUp()
        {
            _buildersList = new List<ISectionBuilder> { new HeaderBuilder(), new FooterBuilder() };
            
            var headerContentMock = new PublishedContentMockingHelper();
            headerContentMock.SetAlias(DocumentTypes.Header);
            var footerContentMock = new PublishedContentMockingHelper();
            footerContentMock.SetAlias(DocumentTypes.Footer);
            var nodeHelperMock = new Mock<INodeHelper>();
            nodeHelperMock.Setup(x => x.GetContent(7)).Returns(headerContentMock.ContentMock);
            nodeHelperMock.Setup(x => x.GetContent(69)).Returns(footerContentMock.ContentMock);
            
            var websiteNode = new PublishedContentMockingHelper();
            websiteNode.AddProperty("title","test Title");
            websiteNode.AddProperty(DocumentTypes.Header, 7);
            websiteNode.AddProperty(DocumentTypes.Footer, 69);
            _websiteNode = websiteNode.ContentMock;

            _pageExtender = new PageModelExtender(nodeHelperMock.Object, _buildersList);
        }

        [Test]
        public void ShouldExtendViewModelWithHeaderTitleAndFooter()
        {
            var extendedModel = _pageExtender.ApplyLayoutToModel(new PageViewModel(), _websiteNode);

            Assert.AreEqual("test Title", extendedModel.Title);
            Assert.IsTrue(extendedModel.Header.ViewModel is HeaderViewModel);
            Assert.IsTrue(extendedModel.Footer.ViewModel is FooterVIewModel);
        }
    }
}
