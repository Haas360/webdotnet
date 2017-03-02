﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Umbraco.Core.Models;
using Webdotnet.Custom.Core;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.Core.Sections;
using Webdotnet.Custom.ViewModels;
using Webdotnet.Tests.TestHeplers;

namespace Webdotnet.Tests
{

    [TestFixture]
    public class SectionProviderTests
    {
        private List<ISectionBuilder> _buildersList;
        private SectionsProvider _sectionProvider;
        private List<IPublishedContent> _mocekdContents;

        [SetUp]
        public void SetUp()
        {
            _buildersList = new List<ISectionBuilder> {new HeaderBuilder(), new FooterBuilder()};
            _sectionProvider = new SectionsProvider(_buildersList);

            var mockContentHeader = new PublishedContentMockingHelper();
            mockContentHeader.SetAlias(DocumentTypes.Header);

            var mockContentFooter = new PublishedContentMockingHelper();
            mockContentFooter.SetAlias(DocumentTypes.Footer);

            _mocekdContents = new List<IPublishedContent>{ mockContentHeader.ContentMock, mockContentFooter.ContentMock };
        }
        [Test]
        public void ShouldReturnProperNumberOfSection()
        {
            Assert.AreEqual(2, _sectionProvider.GetListOfSectionsToRender(_mocekdContents).Count);
        }
        [Test]
        public void ShouldReturnCorrectSection()
        {
            var sections = _sectionProvider.GetListOfSectionsToRender(_mocekdContents);
            var headerSection = sections.FirstOrDefault(x => x.ViewModel is HeaderViewModel);
            var footerSection = sections.FirstOrDefault(x => x.ViewModel is FooterVIewModel);

            Assert.NotNull(headerSection);
            Assert.NotNull(footerSection);
        }
        [Test]
        public void ShouldContainErrorViewModelWhenThereIsntAnyBuilderForAlias()
        {
            var contentWithoutBuilder = new PublishedContentMockingHelper();
            contentWithoutBuilder.SetAlias("Alias without builder");
            _mocekdContents.Add(contentWithoutBuilder.ContentMock);
            var sections = _sectionProvider.GetListOfSectionsToRender(_mocekdContents);

            Assert.IsTrue(sections.Any(x=>x.ViewModel is SectionErrorViewModel));
        }
        [Test]
        public void ShouldRenderUsingFirstBuilderWhenManyApplyToOneAlias()
        {
            _buildersList.Insert(0, new FakeBuilder());
            var sectionProviders = new SectionsProvider(_buildersList);
    
            var sections = sectionProviders.GetListOfSectionsToRender(_mocekdContents);
            Assert.IsTrue(sections.Any(x=>x.ViewModel is FakeViewModel));
            Assert.IsFalse(sections.Any(x=>x.ViewModel is HeaderViewModel));
        }

        [Test]
        public void ShouldContainErrorViewModelInsteadProperWhenExceptionHappen()
        {
            var builders = _buildersList.Where(x => !x.DeosApply(DocumentTypes.Header)).ToList();
            builders.Add(new FakeBuilderWhichReturnsException());
            var sectionProviders = new SectionsProvider(builders);
            var sections = sectionProviders.GetListOfSectionsToRender(_mocekdContents);

            Assert.IsTrue(sections.Any(x => x.ViewModel is SectionErrorViewModel));

            var model = sections.First(x => x.ViewModel is SectionErrorViewModel).ViewModel;
            var errorViewModel = (SectionErrorViewModel)model;
            Assert.AreEqual("message of exception", errorViewModel.ErrorMsg);
        }
    }
    public class FakeBuilder : ISectionBuilder
    {
        public string ViewName => "";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            return new FakeViewModel();
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == DocumentTypes.Header;
        }
    }
    public class FakeBuilderWhichReturnsException : ISectionBuilder
    {
        public string ViewName => "";
        public BaseViewModel CreateViewModel(IPublishedContent content)
        {
            var exception = new Exception("message of exception");
            throw exception;
        }

        public bool DeosApply(string documentAlias)
        {
            return documentAlias == DocumentTypes.Header;
        }
    }
    public class FakeViewModel : BaseViewModel { }
}
