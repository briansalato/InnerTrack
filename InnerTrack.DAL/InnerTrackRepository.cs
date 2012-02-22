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
            using (var db = GetInnerTrackEntities())
            {
                var items = db.Projects.AsQueryable();

                if (filter.Id.HasValue)
                {
                    items = items.Where(i => i.Id == filter.Id.Value);
                }

                if (!string.IsNullOrEmpty(filter.OwnersUserName))
                {
                    items = items.Where(i => i.User_Project.Any(up => up.User.Email == filter.OwnersUserName));
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

                return ToObjs(items);
            }
        }

        public int CreateProject(ProjectObj proj, string username)
        {
            using (var db = GetInnerTrackEntities())
            {
                var dbItem = ToDbItem(proj);
                var user = GetOrCreateCreateUser(username, db);
                dbItem.User_Project.Add(new User_Project()
                {
                    UserId = user.Id,
                    CreatedBy = username,
                    CreatedOn = DateTime.Now
                });
                dbItem.CreatedBy = username;
                dbItem.CreatedOn = DateTime.Now;

                db.Projects.AddObject(dbItem);

                db.SaveChanges();

                return dbItem.Id;
            }
        }

        public bool UpdateProject(ProjectObj proj, string username)
        {
            if (!proj.Id.HasValue)
            {
                return false;
            }
            using (var db = GetInnerTrackEntities())
            {
                var dbItem = db.Projects.SingleOrDefault(i => i.Id == proj.Id.Value);
                if (dbItem == null)
                {
                    return false;
                }

                dbItem.Name = proj.Name;
                dbItem.Description = proj.Description;
                dbItem.UpdatedBy = username;
                dbItem.UpdatedOn = DateTime.Now;
                db.SaveChanges();

                return true;
            }
        }

        #region -Converters

        private IList<ProjectObj> ToObjs(IEnumerable<Project> items)
        {
            var objs = new List<ProjectObj>();
            foreach (var item in items)
            {
                objs.Add(ToObj(item));
            }

            return objs;
        }

        private ProjectObj ToObj(Project item)
        {
            var obj = new ProjectObj
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            };

            return obj;
        }

        private Project ToDbItem(ProjectObj obj)
        {
            var dbItem = new Project
            {
                Name = obj.Name,
                Description = obj.Description
            };

            if (obj.Id.HasValue)
            {
                dbItem.Id = obj.Id.Value;
            }

            return dbItem;
        }
        #endregion
        #endregion

        #region -Feeds
        public IList<FeedObj> GetFeeds(FeedFilter filter)
        {
            using (var db = GetInnerTrackEntities())
            {
                var items = db.Feeds.AsQueryable();

                if (filter.Id.HasValue)
                {
                    items = items.Where(i => i.Id == filter.Id.Value);
                }

                if (filter.ProjectId.HasValue)
                {
                    items = items.Where(i => i.Project_Feed.Any(pf => pf.ProjectId == filter.ProjectId.Value));
                }

                return ToObjs(items);
            }
        }

        public int CreateFeed(FeedObj feed, string username)
        {
            using (var db = GetInnerTrackEntities())
            {
                var now = DateTime.Now;

                var dbItem = ToDbItem(feed);
                dbItem.CreatedBy = username;
                dbItem.CreatedOn = now;

                db.Feeds.AddObject(dbItem);

                foreach (var proj in feed.Projects)
                {
                    var dbProjFeed = new Project_Feed()
                    {
                        FeedId = dbItem.Id,
                        ProjectId = proj.Id.Value
                    };
                    dbProjFeed.CreatedBy = username;
                    dbProjFeed.CreatedOn = now;
                    db.Project_Feed.AddObject(dbProjFeed);
                }

                db.SaveChanges();

                return dbItem.Id;
            }
        }

        #region -Converters
        private IList<FeedObj> ToObjs(IEnumerable<Feed> items)
        {
            var objs = new List<FeedObj>();
            foreach (var item in items)
            {
                objs.Add(ToObj(item));
            }

            return objs;
        }

        private FeedObj ToObj(Feed item)
        {
            var obj = new FeedObj
            {
                Id = item.Id,
                Type = ToObj(item.FeedType)
            };

            return obj;
        }

        private Feed ToDbItem(FeedObj obj)
        {
            var dbItem = new Feed
            {
            };

            if (obj.Id.HasValue)
            {
                dbItem.Id = obj.Id.Value;
            }

            return dbItem;
        }
        #endregion
        #endregion

        #region -Tags
        public IList<TagObj> GetTags(TagFilter filter)
        {
            using (var db = GetInnerTrackEntities())
            {
                var items = db.Tags.AsQueryable();

                if (filter.Id.HasValue)
                {
                    items = items.Where(i => i.Id == filter.Id.Value);
                }

                if (filter.ProjectId.HasValue)
                {
                    items = items.Where(i => i.Project_Tag.Any(pf => pf.ProjectId == filter.ProjectId.Value));
                }

                return ToObjs(items);
            }
        }

        #region -Converters

        private IList<TagObj> ToObjs(IEnumerable<Tag> items)
        {
            var objs = new List<TagObj>();
            foreach (var item in items)
            {
                objs.Add(ToObj(item));
            }

            return objs;
        }

        private TagObj ToObj(Tag item)
        {
            var obj = new TagObj
            {
                Id = item.Id,
                Name = item.Name
            };

            return obj;
        }
        #endregion
        #endregion

        #region -ProjectSourceConfigs
        public IList<ProjectSourceTypeObj> GetProjectSourceTypes(ProjectSourceTypeFilter filter)
        {
            using (var db = GetInnerTrackEntities())
            {
                var items = db.ProjectSourceTypes.AsQueryable();

                if (filter.Id.HasValue)
                {
                    items = items.Where(i => i.Id == filter.Id.Value);
                }

                /*
                 * Any Addtional Filtering is done here based on if properties in the filter are not null
                 */

                return ToObjs(items);
            }
        }

        public int CreateProjectSourceType(ProjectSourceTypeObj obj, string username)
        {
            using (var db = GetInnerTrackEntities())
            {
                var dbItem = ToDbItem(obj);
                dbItem.CreatedBy = username;
                dbItem.CreatedOn = DateTime.Now;

                db.ProjectSourceTypes.AddObject(dbItem);

                db.SaveChanges();

                return dbItem.Id;
            }
        }

        public bool UpdateProjectSourceType(ProjectSourceTypeObj obj, string username)
        {
            if (!obj.Id.HasValue)
            {
                return false;
            }
            using (var db = GetInnerTrackEntities())
            {
                var dbItem = db.ProjectSourceTypes.SingleOrDefault(i => i.Id == obj.Id.Value);
                if (dbItem == null)
                {
                    return false;
                }

                dbItem.Name = obj.Name;
                dbItem.FullClassName = obj.FullClassName;

                if (obj.NextRun.HasValue)
                {
                    dbItem.NextRun = obj.NextRun.Value < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value : obj.NextRun.Value;
                }
                if (obj.LastRun.HasValue)
                {
                    dbItem.LastRun = obj.LastRun.Value;
                }
                dbItem.Enabled = obj.Enabled;
                dbItem.Interval = obj.Interval;
                dbItem.UpdatedBy = username;
                dbItem.UpdatedOn = DateTime.Now;
                db.SaveChanges();

                return true;
            }
        }

        #region -Converters
        private IList<ProjectSourceTypeObj> ToObjs(IEnumerable<ProjectSourceType> items)
        {
            var objs = new List<ProjectSourceTypeObj>();
            foreach (var item in items)
            {
                objs.Add(ToObj(item));
            }

            return objs;
        }

        private ProjectSourceTypeObj ToObj(ProjectSourceType item)
        {
            var obj = new ProjectSourceTypeObj
            {
                Id = item.Id,
                Name = item.Name,
                FullClassName = item.FullClassName,
                Interval = item.Interval,
                LastRun = item.LastRun,
                NextRun = item.NextRun,
                Enabled = item.Enabled
            };

            return obj;
        }

        private ProjectSourceType ToDbItem(ProjectSourceTypeObj obj)
        {
            var dbItem = new ProjectSourceType
            {
                Name = obj.Name,
                FullClassName = obj.FullClassName,
                Interval = obj.Interval,
                LastRun = obj.LastRun,
                NextRun = obj.NextRun.Value,
                Enabled = obj.Enabled
            };

            if (obj.Id.HasValue)
            {
                dbItem.Id = obj.Id.Value;
            }

            return dbItem;
        }
        #endregion

        #endregion

        #region -ProjectSources
        public IList<ProjectSourceObj> GetProjectSources(ProjectSourceFilter filter)
        {
            using (var db = GetInnerTrackEntities())
            {
                var items = db.ProjectSources.AsQueryable();

                if (filter.Id.HasValue)
                {
                    items = items.Where(i => i.Id == filter.Id.Value);
                }

                /*
                 * Any Addtional Filtering is done here based on if properties in the filter are not null
                 */

                return ToObjs(items);
            }
        }

        public int CreateProjectSource(ProjectSourceObj obj, string username)
        {
            using (var db = GetInnerTrackEntities())
            {
                var dbItem = ToDbItem(obj);
                dbItem.CreatedBy = username;
                dbItem.CreatedOn = DateTime.Now;

                db.ProjectSources.AddObject(dbItem);

                db.SaveChanges();

                return dbItem.Id;
            }
        }

        public bool UpdateProjectSource(ProjectSourceObj obj, string username)
        {
            if (!obj.Id.HasValue)
            {
                return false;
            }
            using (var db = GetInnerTrackEntities())
            {
                var dbItem = db.ProjectSources.SingleOrDefault(i => i.Id == obj.Id.Value);
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

        #region -Converters
        private IList<ProjectSourceObj> ToObjs(IEnumerable<ProjectSource> items)
        {
            var objs = new List<ProjectSourceObj>();
            foreach (var item in items)
            {
                objs.Add(ToObj(item));
            }

            return objs;
        }

        private ProjectSourceObj ToObj(ProjectSource item)
        {
            var obj = new ProjectSourceObj
            {
                Id = item.Id
            };

            return obj;
        }

        private ProjectSource ToDbItem(ProjectSourceObj obj)
        {
            var dbItem = new ProjectSource
            {
            };

            if (obj.Id.HasValue)
            {
                dbItem.Id = obj.Id.Value;
            }

            return dbItem;
        }
        #endregion
        #endregion

        #region -Users
        private User GetOrCreateCreateUser(string username, InnerTrackEntities db)
        {
            User dbItem = db.Users.FirstOrDefault(u => u.Username == username);
            if (dbItem == null)
            {
                dbItem = new User()
                {
                    Email = username,
                    Username = username
                };

                db.Users.AddObject(dbItem);

                db.SaveChanges();
            }

            return dbItem;
        }
        #endregion

        #region -Converters

        private FeedTypeObj ToObj(FeedType item)
        {
            var obj = new FeedTypeObj
            {
                Id = item.Id,
                Name = item.Name,
                AssociatedClass = item.AssociatedClass
            };

            return obj;
        }
        #endregion

        private InnerTrackEntities GetInnerTrackEntities()
        {
            return new InnerTrackEntities();
        }
    }
}
