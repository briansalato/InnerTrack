using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Interfaces.Logic;
using InnerTrack.Common.Objs;
using InnerTrack.Common.Interfaces.Database;
using InnerTrack.Common.Filters;
using InnerTrack.DAL;
using System.Diagnostics.CodeAnalysis;

namespace InnerTrack.BLL.Logic
{
    public class ProjectLogic : BaseLogic, IProjectLogic
    {
        public ProjectObj Get(int id)
        {
            var filter = new ProjectFilter
            {
                Id = id
            };
            
            var obj = Repository.GetProjects(filter).SingleOrDefault();

            if (obj != null)
            {
                var feedLogic = new FeedLogic(Repository);
                obj.Feeds = feedLogic.GetForProject(obj.Id.Value);

                var tagLogic = new TagLogic(Repository);
                obj.Tags = tagLogic.GetForProject(obj.Id.Value);
            }

            return obj;
        }

        public IList<ProjectObj> GetByOwner(string username)
        {
            var filter = new ProjectFilter()
            {
                OwnersUserName = username
            };
            return Repository.GetProjects(filter);
        }

        public IList<ProjectObj> GetAll()
        {
            var filter = new ProjectFilter();
            return Repository.GetProjects(filter);
        }

        public SearchResult<ProjectObj> Search(string query, int curPage, int pageSize)
        {
            var splitQuery = query.Split(' ');
            var filter = new ProjectFilter
            {
                QueryNames = splitQuery,
                QueryDescriptions = splitQuery,
                StartIndex = curPage * pageSize,
                MaxResults = pageSize + 1 //we do +1 so we can see if the next button will be enabled
            };

            var objs = Repository.GetProjects(filter);

            SearchResult<ProjectObj> result = new SearchResult<ProjectObj>();
            result.HasMore = objs.Count > pageSize;
            if (result.HasMore)
            {
                //we remove the last object because it was only fetched to see if has more is enabled
                objs.RemoveAt(objs.Count - 1);
            }
            result.HasPrevious = curPage > 0;

            result.Results = objs;

            return result;

        }

        public int Create(ProjectObj proj, string username)
        {
            return Repository.CreateProject(proj, username);
        }

        public bool Update(ProjectObj proj, string username)
        {
            return Repository.UpdateProject(proj, username);
        }

        [ExcludeFromCodeCoverage]
        public ProjectLogic() : base(new InnerTrackRepository()) { }

        [ExcludeFromCodeCoverage]
        public ProjectLogic(IInnerTrackRepository repository) :base(repository) {}
    }
}
