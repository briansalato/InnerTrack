using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Interfaces.Logic;
using InnerTrack.Common.Objs;
using System.Diagnostics.CodeAnalysis;
using InnerTrack.Common.Interfaces.Database;
using InnerTrack.DAL;
using InnerTrack.Common.Filters;
using InnerTrack.Common.Enumerations;

namespace InnerTrack.BLL.Logic
{
    public class FeedLogic : BaseLogic, IFeedLogic
    {
        public FeedObj Get(int id)
        {
            var filter = new FeedFilter
            {
                Id = id
            };

            var obj = Repository.GetFeeds(filter).SingleOrDefault();

            return obj;
        }

        public IList<FeedObj> GetForProject(int projectId)
        {
            var filter = new FeedFilter()
            {
                ProjectId = projectId
            };
            return Repository.GetFeeds(filter);
        }

        public int Create(FeedObj obj, string username)
        {
            return Repository.CreateFeed(obj, username);
        }

        public IEnumerable<Common.Enumerations.FeedType> GetFeedTypes()
        {
            return Enum.GetValues(typeof(Common.Enumerations.FeedType)).Cast<Common.Enumerations.FeedType>();
        }

        [ExcludeFromCodeCoverage]
        public FeedLogic() : base(new InnerTrackRepository()) { }

        [ExcludeFromCodeCoverage]
        public FeedLogic(IInnerTrackRepository repository) : base(repository) { }
    }
}
