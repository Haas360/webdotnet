using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.SectionBuilder;
using Webdotnet.Custom.ViewModels;

namespace Webdotnet.Custom.Core
{
    public interface ISectionsProvider
    {
        List<PageSection> GetListOfSectionsToRender(List<IPublishedContent> allSections);
    }

    public class SectionsProvider : ISectionsProvider
    {
        private readonly IBuildersFactory _buildersFactory;

        public SectionsProvider(IBuildersFactory buildersFactory)
        {
            _buildersFactory = buildersFactory;
        }

        public List<PageSection> GetListOfSectionsToRender(List<IPublishedContent> allSections)
        {
            var listOfSectionsToRender = new List<PageSection>();
            if (allSections.IsNotNullOrEmpty())
            {
                foreach (var section in allSections)
                {
                    try
                    {
                        var docAlias = section.DocumentTypeAlias;
                        var sectionBuilder = _buildersFactory.GetFirstBuilderThatApply(docAlias);
                        listOfSectionsToRender.Add(new PageSection
                        {
                            PartialPath = sectionBuilder.ViewName,
                            ViewModel = sectionBuilder.CreateViewModel(section)
                        });
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error<PageSection>(" Error during rendering section", ex);
                        listOfSectionsToRender.Add(new PageSection
                        {
                            PartialPath = Consts.SectionErrorViewName,
                            ViewModel = new SectionErrorViewModel {SectionName = section.Name, ErrorMsg = ex.Message}
                        });
                    }
                }
            }
            return listOfSectionsToRender;
        }
    }
}