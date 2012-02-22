using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnerTrack.Common.Objs;
using InnerTrack.Web.Models;
using System.Diagnostics.CodeAnalysis;
using InnerTrack.Common.Enumerations;

namespace InnerTrack.Web.Helpers
{
    [ExcludeFromCodeCoverage]
    internal static class ModelConverter
    {
        #region -Project Models
        internal static EditProjectModel ConvertToEdit(ProjectObj obj)
        {
            return new EditProjectModel
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Tags = Convert(obj.Tags)
            };
        }

        internal static ShowProjectModel ConvertToShow(ProjectObj obj)
        {
            var model =  new ShowProjectModel
            {
                Id = obj.Id.Value,
                Name = obj.Name,
                Description = obj.Description,
                Feeds = Convert(obj.Feeds),
                Tags = Convert(obj.Tags)
            };

            return model;
        }

        internal static IList<ListItemProjectModel> Convert(IList<ProjectObj> objs)
        {
            var model = new List<ListItemProjectModel>();
            if (objs != null)
            {
                foreach (var obj in objs)
                {
                    model.Add(new ListItemProjectModel()
                    {
                        Id = obj.Id.Value,
                        Name = obj.Name
                    });
                }
            }

            return model;
        }

        internal static ProjectObj Convert(EditProjectModel model)
        {
            return new ProjectObj
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }

        internal static IList<BasicSearchResultModel> ConvertToSearch(IList<ProjectObj> objs)
        {
            var model = new List<BasicSearchResultModel>();
            if (objs != null)
            {
                foreach (var obj in objs)
                {
                    model.Add(new BasicSearchResultModel()
                    {   
                        Id = obj.Id.Value,
                        Title = obj.Name,
                        Description = obj.Description
                    });
                }
            }

            return model;
        }
        #endregion

        #region -Feed Models
        internal static ShowFeedModel ConvertToShow(FeedObj obj)
        {
            return new ShowFeedModel
            {
                Id = obj.Id.Value,
                Type = (FeedType)obj.Type.Id,
                Events = Convert(obj.Events)
            };
        }

        internal static IList<ListItemFeedModel> Convert(IList<FeedObj> objs)
        {
            var model = new List<ListItemFeedModel>();
            if (objs != null)
            {
                foreach (var obj in objs)
                {
                    model.Add(new ListItemFeedModel()
                    {
                        Id = obj.Id.Value,
                        Type = obj.Type.Name
                    });
                }
            }

            return model;
        }

        internal static FeedObj Convert(EditFeedModel model)
        {
            return new FeedObj
            {
                Id = model.Id,
                Type = new FeedTypeObj { Id = (int)model.Type },
                Projects = new List<ProjectObj>
                {
                    new ProjectObj 
                    {
                        Id = model.ProjectId
                    }
                }
            };
        }
        #endregion

        #region -Event Models
        internal static IList<ListItemEventModel> Convert(IList<EventObj> objs)
        {
            var model = new List<ListItemEventModel>();
            if (objs != null)
            {
                foreach (var obj in objs)
                {
                    model.Add(new ListItemEventModel() {});
                }
            }

            return model;
        }
        #endregion

        #region -Tag Models
        internal static IList<ListItemTagModel> Convert(IList<TagObj> objs)
        {
            var model = new List<ListItemTagModel>();
            if (objs != null)
            {
                foreach (var obj in objs)
                {
                    model.Add(new ListItemTagModel() 
                    { 
                        Id = obj.Id.Value,
                        Name = obj.Name
                    });
                }
            }

            return model;
        }
        #endregion

        #region -ProjectSourceType Models
        internal static EditProjectSourceTypeModel ConvertToEdit(ProjectSourceTypeObj obj)
        {
            return new EditProjectSourceTypeModel
            {
                Id = obj.Id,
                Name = obj.Name,
                FullClassName = obj.FullClassName,
                Interval = obj.Interval,
                Enabled = obj.Enabled
            };
        }

        internal static ShowProjectSourceTypeModel ConvertToShow(ProjectSourceTypeObj obj)
        {
            var model = new ShowProjectSourceTypeModel
            {
                Id = obj.Id.Value,
                Name = obj.Name,
                FullClassName = obj.FullClassName,
                Interval = obj.Interval,
                LastRun = obj.LastRun,
                NextRun = obj.NextRun.Value,
                Enabled = obj.Enabled
            };

            return model;
        }

        internal static IList<ListItemProjectSourceTypeModel> Convert(IList<ProjectSourceTypeObj> objs)
        {
            var model = new List<ListItemProjectSourceTypeModel>();
            if (objs != null)
            {
                foreach (var obj in objs)
                {
                    model.Add(new ListItemProjectSourceTypeModel()
                    {
                        Id = obj.Id.Value,
                        Name = obj.Name,
                        NextRun = obj.NextRun.Value,
                        Enabled = obj.Enabled
                    });
                }
            }

            return model;
        }

        internal static ProjectSourceTypeObj Convert(EditProjectSourceTypeModel model)
        {
            return new ProjectSourceTypeObj
            {
                Id = model.Id,
                Name = model.Name,
                FullClassName = model.FullClassName,
                Interval = model.Interval,
                NextRun = model.ForceRun ? DateTime.MinValue : (DateTime?)null,
                Enabled = model.Enabled
            };
        }
        #endregion

        #region -ProjectSource Models
        internal static EditProjectSourceModel ConvertToEdit(ProjectSourceObj obj)
        {
            return new EditProjectSourceModel
            {
                Id = obj.Id
                /*
                 * Copy other properties here
                 */
            };
        }

        internal static ShowProjectSourceModel ConvertToShow(ProjectSourceObj obj)
        {
            var model = new ShowProjectSourceModel
            {
                Id = obj.Id.Value
                /*
                 * Copy other properties here
                 */
            };

            return model;
        }

        internal static IList<ListItemProjectSourceModel> Convert(IList<ProjectSourceObj> objs)
        {
            var model = new List<ListItemProjectSourceModel>();
            if (objs != null)
            {
                foreach (var obj in objs)
                {
                    model.Add(new ListItemProjectSourceModel()
                    {
                        Id = obj.Id.Value,
                        Configuration = obj.Configuration,
                        TypeName = obj.SourceConfig.Name
                    });
                }
            }

            return model;
        }

        internal static ProjectSourceObj Convert(EditProjectSourceModel model)
        {
            return new ProjectSourceObj
            {
                Id = model.Id
                /*
                 * Copy other properties here
                 */
            };
        }
        #endregion
    }
}