using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using InnerTrack.Common.Interfaces.Logic;
using InnerTrack.Common.Objs;
using InnerTrack.Common.Interfaces.Database;
using InnerTrack.Common.Filters;
using InnerTrack.DAL;

namespace InnerTrack.BLL.Logic
{
    public class ProjectSourceLogic : BaseLogic, IProjectSourceLogic
    {
        public ProjectSourceObj Get(int id)
        {
            var filter = new ProjectSourceFilter
            {
                Id = id
            };
            
            var obj = Repository.GetProjectSources(filter).SingleOrDefault();

            /* Any Complex Properties that need to be filled in through more DB calls
            if (obj != null)
            {
                
            }
            */

            return obj;
        }

        public IList<ProjectSourceObj> GetAll()
        {
            var filter = new ProjectSourceFilter();
            return Repository.GetProjectSources(filter);
        }

        public int Create(ProjectSourceObj obj, string username)
        {
            return Repository.CreateProjectSource(obj, username);
        }

        public bool Update(ProjectSourceObj obj, string username)
        {
            return Repository.UpdateProjectSource(obj, username);
        }

        [ExcludeFromCodeCoverage]
        public ProjectSourceLogic() : base(new InnerTrackRepository()) { }

        [ExcludeFromCodeCoverage]
        public ProjectSourceLogic(IInnerTrackRepository repository) :base(repository) {}
    }
}
