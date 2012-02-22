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
    public class ProjectSourceTypeLogic : BaseLogic, IProjectSourceTypeLogic
    {
        public ProjectSourceTypeObj Get(int id)
        {
            var filter = new ProjectSourceTypeFilter
            {
                Id = id
            };
            
            var obj = Repository.GetProjectSourceTypes(filter).SingleOrDefault();

            /* Any Complex Properties that need to be filled in through more DB calls
            if (obj != null)
            {
                
            }
            */

            return obj;
        }

        public IList<ProjectSourceTypeObj> GetAll()
        {
            var filter = new ProjectSourceTypeFilter();
            return Repository.GetProjectSourceTypes(filter);
        }

        public int Create(ProjectSourceTypeObj obj, string username)
        {
            if (!obj.NextRun.HasValue) 
            { 
                obj.NextRun = DateTime.Now.AddMinutes(obj.Interval);
            }
            return Repository.CreateProjectSourceType(obj, username);
        }

        public bool Update(ProjectSourceTypeObj obj, string username)
        {
            return Repository.UpdateProjectSourceType(obj, username);
        }

        [ExcludeFromCodeCoverage]
        public ProjectSourceTypeLogic() : base(new InnerTrackRepository()) { }

        [ExcludeFromCodeCoverage]
        public ProjectSourceTypeLogic(IInnerTrackRepository repository) :base(repository) {}
    }
}
