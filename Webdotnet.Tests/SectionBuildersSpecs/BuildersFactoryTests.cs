using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.Core.Sections;

namespace Webdotnet.Tests.SectionBuildersSpecs
{
    [TestFixture]
    public class BuildersFactoryTests
    {
        protected BuildersFactory BuildersFactory;

        [SetUp]
        public void SetUp()
        {
            var builders = new List<ISectionBuilder>()
            {
                new ErrorSectionBuilder(),
                new FakeBuilder(),
                new FooterBuilder(),
                new HeaderBuilder()
            };
            
            BuildersFactory = new BuildersFactory(builders);
        }
    }

    public class WhenRequestIsForSingleBuilder : BuildersFactoryTests
    {
        [Test]
        public void ShouldReturnOnlyOneBuilderWhichApplyToDoctypeAlias()
        {
            var builder = BuildersFactory.GetFirstBuilderThatApply(DocumentTypes.Header);
            Assert.True(builder is FakeBuilder);
        }
        [Test]
        public void ShouldReturnErrorBuilderWhenDidntFindBuilderForAlias()
        {
            var builder = BuildersFactory.GetFirstBuilderThatApply("I'm Sure there is no builder for that");
            Assert.True(builder is ErrorSectionBuilder);
        }
    }

    public class WhenRequestIsForMultipleBuilders : BuildersFactoryTests
    {
        [Test]
        public void ShouldReturnListOfBuilders()
        {
            var builders = BuildersFactory.GetAllBuildersThatApply(DocumentTypes.Header);
            Assert.AreEqual(2, builders.Count);

        }
        [Test]
        public void ShouldReturnOnlyAllBuilderWhichApplyToDoctypeAlias()
        {
            var builders = BuildersFactory.GetAllBuildersThatApply(DocumentTypes.Header);
            Assert.AreEqual(2, builders.Count);
            Assert.AreEqual(1, builders.Count(x => x is HeaderBuilder));
            Assert.AreEqual(1, builders.Count(x => x is FakeBuilder));
        }
        [Test]
        public void ShouldReturnErrorBuilderWhenDidntFindBuilderForAlias()
        {
            var builders = BuildersFactory.GetAllBuildersThatApply("I'm Sure there is no builder for that");
            Assert.AreEqual(1, builders.Count);
            Assert.AreEqual(1, builders.Count(x => x is ErrorSectionBuilder));
        }

        [Test]
        public void ShouldntContainErrorBuilderWhenThereIsABuilderForAlias()
        {
            var builders = BuildersFactory.GetAllBuildersThatApply(DocumentTypes.Footer);
            Assert.AreEqual(1, builders.Count);
            Assert.AreEqual(0, builders.Count(x => x is ErrorSectionBuilder));
        }
    }
}
