using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Interfaces.Database;
using InnerTrack.Common.Objs;
using InnerTrack.Common.Filters;
using InnerTrack.Common.Enumerations;
using System.Data.SqlTypes;

namespace InnerTrack.DAL
{
    public class InnerTrackRepository : IInnerTrackRepository
    {
        #region -Projects
        public IList<ProjectObj> GetProjects(ProjectFilter filter)
        {
            using (var db = GetInnerTrackContext())
            {
                var items = db.Projects.AsQueryable();

                if (filter.Id.HasValue)
                {
                    items = items.Where(i => i.Id == filter.Id.Value);
                }

                if (!string.IsNullOrEmpty(filter.OwnersUserName))
                {
                    items = items.Where(i => i.Members.Any(m => m.Email == filter.OwnersUserName));
                }

                if (filter.QueryNames != null)
                {
                    items = items.Where(i => filter.QueryNames.Any(q => i.Name.Contains(q)));
                }

                if (filter.QueryDescriptions != null)
                {
                    items = items.Where(i => filter.QueryDescriptions.Any(q => i.Name.Contains(q)));
                }

                if (filter.StartIndex.HasValue)
                {
                    items = items.OrderBy(i => i.Id).Skip(filter.StartIndex.Value);
                }

                if (filter.MaxResults.HasValue)
                {
                    items = items.Take(filter.MaxResults.Value);
                }

                return items.ToList();
            }
        }

        public int CreateProject(ProjectObj item, string username)
        {
            using (var db = GetInnerTrackContext())
            {
                var user = GetOrCreateCreateUser(username, db);
                item.Members = new List<UserObj>();
                item.Members.Add(user);
                item.CreatedBy = username;
                item.CreatedOn = DateTime.Now;

                db.Projects.Add(item);

                db.SaveChanges();

                return item.Id.Value;
            }
        }

        public bool UpdateProject(ProjectObj item, string username)
        {
            if (!item.Id.HasValue)
            {
                return false;
            }
            using (var db = GetInnerTrackContext())
            {
                var dbItem = db.Projects.SingleOrDefault(i => i.Id == item.Id.Value);
                if (dbItem == null)
                {
                    return false;
                }

                dbItem.Name = item.Name;
                dbItem.Description = item.Description;
                dbItem.UpdatedBy = username;
                dbItem.UpdatedOn = DateTime.Now;
                db.SaveChanges();

                return true;
            }
        }
        #endregion

        #region -Feeds
        public IList<FeedObj> GetFeeds(FeedFilter filter)
        {
            using (var db = GetInnerTrackContext())
            {
                var items = db.Feeds.AsQueryable();

                if (filter.Id.HasValue)
                {
                    items = items.Where(i => i.Id == filter.Id.Value);
                }

                if (filter.ProjectId.HasValue)
                {
                    items = items.Where(i => i.Projects.Any(p => p.Id == filter.ProjectId.Value));
                }

                return items.ToList();
            }
        }

        public int CreateFeed(FeedObj item, string username)
        {
            using (var db = GetInnerTrackContext())
            {
                var now = DateTime.Now;

                item.CreatedBy = username;
                item.CreatedOn = now;

                db.Feeds.Add(item);

                db.SaveChanges();

                return item.Id.Value;
            }
        }
        #endregion

        #region -Tags
        public IList<TagObj> GetTags(TagFilter filter)
        {
            using (var db = GetInnerTrackContext())
            {
                var items = db.Tags.AsQueryable();

                if (filter.Id.HasValue)
                {
                    items = items.Where(i => i.Id == filter.Id.Value);
                }

                if (filter.ProjectId.HasValue)
                {
                    items = items.Where(i => db.Projects.Where(p => p.Id == filter.ProjectId).Any(p => p.Tags.Contains(i)));
                }

                return items.ToList();
            }
        }
        #endregion

        #region -ProjectSourceConfigs
        public IList<ProjectSourceTypeObj> GetProjectSourceTypes(ProjectSourceTypeFilter filter)
        {
            using (var db = GetInnerTrackContext())
            {
                var items = db.ProjectSourceTypes.AsQueryable();

                if (filter.Id.HasValue)
                {
                    items = items.Where(i => i.Id == filter.Id.Value);
                }

                /*
                 * Any Addtional Filtering is done here based on if properties in the filter are not null
                 */

                return items.ToList();
            }
        }

        public int CreateProjectSourceType(ProjectSourceTypeObj item, string username)
        {
            using (var db = GetInnerTrackContext())
            {
                item.CreatedBy = username;
                item.CreatedOn = DateTime.Now;

                db.ProjectSourceTypes.Add(item);

                db.SaveChanges();

                return item.Id.Value;
            }
        }

        public bool UpdateProjectSourceType(ProjectSourceTypeObj item, string username)
        {
            if (!item.Id.HasValue)
            {
                return false;
            }
            using (var db = GetInnerTrackContext())
            {
                var dbItem = db.ProjectSourceTypes.SingleOrDefault(i => i.Id == item.Id.Value);
                if (dbItem == null)
                {
                    return false;
                }

                dbItem.Name = item.Name;
                dbItem.FullClassName = item.FullClassName;

                if (item.NextRun.HasValue)
                {
                    dbItem.NextRun = item.NextRun.Value < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value : item.NextRun.Value;
                }
                if (item.LastRun.HasValue)
                {
                    dbItem.LastRun = item.LastRun.Value;
                }
                dbItem.Enabled = item.Enabled;
                dbItem.Interval = item.Interval;
                dbItem.UpdatedBy = username;
                dbItem.UpdatedOn = DateTime.Now;
                db.SaveChanges();

                return true;
            }
        }

        #endregion

        #region -ProjectSources
        public IList<ProjectSourceObj> GetProjectSources(ProjectSourceFilter filter)
        {
            using (var db = GetInnerTrackContext())
            {
                var items = db.ProjectSources.AsQueryable();

                if (filter.Id.HasValue)
                {
                    items = items.Where(i => i.Id == filter.Id.Value);
                }

                /*
                 * Any Addtional Filtering is done here based on if properties in the filter are not null
                 */

                return items.ToList();
            }
        }

        public int CreateProjectSource(ProjectSourceObj item, string username)
        {
            using (var db = GetInnerTrackContext())
            {
                item.CreatedBy = username;
                item.CreatedOn = DateTime.Now;

                db.ProjectSources.Add(item);

                db.SaveChanges();

                return item.Id.Value;
            }
        }

        public bool UpdateProjectSource(ProjectSourceObj item, string username)
        {
            if (!item.Id.HasValue)
            {
                return false;
            }
            using (var db = GetInnerTrackContext())
            {
                var dbItem = db.ProjectSources.SingleOrDefault(i => i.Id == item.Id.Value);
                if (dbItem == null)
                {
                    return false;
                }

                dbItem.UpdatedBy = username;
                dbItem.UpdatedOn = DateTime.Now;
                db.SaveChanges();

                return true;
            }
        }
        #endregion

        #region -Users
        private UserObj GetOrCreateCreateUser(string username, InnerTrackContext db)
        {
            UserObj dbItem = db.Users.FirstOrDefault(u => u.Email == username);
            if (dbItem == null)
            {
                dbItem = new UserObj()
                {
                    Email = username,
                    CreatedBy = "system",
                    CreatedOn = DateTime.Now
                };

                db.Users.Add(dbItem);

                db.SaveChanges();
            }

            return dbItem;
        }
        #endregion

        private InnerTrackContext GetInnerTrackContext()
        {
            return new InnerTrackContext("InnerTrackDb");
        }
    }
}
