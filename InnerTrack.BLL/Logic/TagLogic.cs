using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Interfaces.Logic;
using InnerTrack.Common.Objs;
using System.Diagnostics.CodeAnalysis;
using InnerTrack.DAL;
using InnerTrack.Common.Interfaces.Database;
using InnerTrack.Common.Filters;

namespace InnerTrack.BLL.Logic
{
    public class TagLogic : BaseLogic, ITagLogic
    {
        public IList<TagObj> GetForProject(int projectId)
        {
            var filter = new TagFilter
            {
                ProjectId = projectId
            };
            return Repository.GetTags(filter);
        }
        
        [ExcludeFromCodeCoverage]
        public TagLogic() : base(new InnerTrackRepository()) { }

        [ExcludeFromCodeCoverage]
        public TagLogic(IInnerTrackRepository repository) : base(repository) { }
    }
}
