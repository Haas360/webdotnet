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
        private readonly List<ISectionBuilder> _sectionBuilders;

        public SectionsProvider(List<ISectionBuilder> sectionBuilders)
        {
            _sectionBuilders = sectionBuilders;
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
                        var sectionBuilder = _sectionBuilders.FirstOrDefault(x => x.DeosApply(docAlias));
                        listOfSectionsToRender.Add(sectionBuilder != null
                            ? new PageSection
                            {
                                PartialPath = sectionBuilder.ViewName,
                                ViewModel = sectionBuilder.CreateViewModel(section)
                            }
                            : new PageSection
                            {
                                PartialPath = Consts.SectionErrorViewName,
                                ViewModel = new SectionErrorViewModel {SectionName = section.Name, ErrorMsg = "You need to create Section builder for this alias"}
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