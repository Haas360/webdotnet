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
        private readonly FooterBuilder _builder = new FooterBuilder(null);
        [Test]
        public void BuilderApplyOnlyToFooterDocType()
        {
            Assert.True(_builder.DeosApply(SectionDocumentTypes.Footer));
        }
    }
}
