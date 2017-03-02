using Moq;
using Umbraco.Core.Models;

namespace Webdotnet.Tests.TestHeplers
{
    public class PublishedContentMockingHelper
    {
        private readonly Mock<IPublishedContent> _contentMock;

        public IPublishedContent ContentMock => _contentMock.Object;

        public PublishedContentMockingHelper()
        {
            _contentMock = new Mock<IPublishedContent>();
        }
        public void AddProperty(string propertyAlias, object propertyValue)
        {
            var mockedProperty = new Mock<IPublishedProperty>();
            mockedProperty.Setup(p => p.Value).Returns(propertyValue);
            mockedProperty.Setup(p => p.HasValue).Returns(true);
            _contentMock.Setup(c => c.GetProperty(propertyAlias, false)).Returns(mockedProperty.Object);
        }

        public void SetAlias(string alias) => _contentMock.Setup(x => x.DocumentTypeAlias).Returns(alias);
    }
}
