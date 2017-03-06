using System.Collections.Generic;
using System.Linq;
using Webdotnet.Custom.Core.Helpers;
using Webdotnet.Custom.Core.Sections;

namespace Webdotnet.Custom.Core.SectionBuilder
{
    public interface IBuildersFactory
    {
        ISectionBuilder GetFirstBuilderThatApply(string doctypeAlias);
        IList<ISectionBuilder> GetAllBuildersThatApply(string doctypeAlias);
    }
    public class BuildersFactory : IBuildersFactory
    {
        private readonly IList<ISectionBuilder> _allBuilders;

        public BuildersFactory(IList<ISectionBuilder> allBuilders)
        {
            _allBuilders = allBuilders;
        }
        public ISectionBuilder GetFirstBuilderThatApply(string doctypeAlias)
        {
            var buildersWhichApply = _allBuilders.Where(x => x.DeosApply(doctypeAlias)).ToList();
            return buildersWhichApply.Any() ? buildersWhichApply.First() : new ErrorSectionBuilder();
        }

        public IList<ISectionBuilder> GetAllBuildersThatApply(string doctypeAlias)
        {
            var buildersWhichApply = _allBuilders.Where(x => x.DeosApply(doctypeAlias)).ToList();
            return buildersWhichApply.Any() ? buildersWhichApply : One.Item((ISectionBuilder)new ErrorSectionBuilder());
        }
     
    }
}
