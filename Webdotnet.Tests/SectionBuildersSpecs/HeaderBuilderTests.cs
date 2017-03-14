using NUnit.Framework;
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
            Assert.True(_builder.DeosApply(SectionDocumentTypes.Header));
        }
    }
}
