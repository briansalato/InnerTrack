using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Objs;
using InnerTrack.Common.Filters;

namespace InnerTrack.Common.Interfaces.Database
{
    public interface IInnerTrackRepository
    {
        #region -Projects
        /// <summary>
        /// Returns a list of objects from the project table
        /// </summary>
        /// <param name="filter">This will filter the query based on any property that is not null</param>
        /// <returns>Returns the list of products found, or a 0 length list if none were found</returns>
        IList<ProjectObj> GetProjects(ProjectFilter filter);

        /// <summary>
        /// Adds a new entry to the project table
        /// </summary>
        /// <param name="proj">The item to add</param>
        /// <returns>The Id of the created item or -1 if no item was created</returns>
        int CreateProject(ProjectObj proj, string username);

        /// <summary>
        /// Updates an entry in the project table
        /// </summary>
        /// <param name="proj">The item to update. It will be updated based on it's Id</param>
        /// <returns>True if the Id was found and the entry updated, or false if the Id was not found or the entry not able to be updated</returns>
        bool UpdateProject(ProjectObj proj, string username);
        #endregion

        #region -Feeds
        /// <summary>
        /// Returns a list of objects from the feed table
        /// </summary>
        /// <param name="filter">This will filter the query based on any property that is not null</param>
        /// <returns>Returns the list of feeds found, or a 0 length list if none were found</returns>
        IList<FeedObj> GetFeeds(FeedFilter filter);

        /// <summary>
        /// Adds a new entry to the feed table
        /// </summary>
        /// <param name="feed">The item to add</param>
        /// <returns>The Id of the created item or -1 if no item was created</returns>
        int CreateFeed(FeedObj feed, string username);
        #endregion

        #region -Tags
        /// <summary>
        /// Returns a list of objects from the tags table
        /// </summary>
        /// <param name="filter">This will filter the query based on any property that is not null</param>
        /// <returns>Returns the list of tags found, or a 0 length list if none were found</returns>
        IList<TagObj> GetTags(TagFilter filter);
        #endregion

        #region -ProjectSourceConfig
        /// <summary>
        /// Returns a list of objects from the ProjectSourceConfig table
        /// </summary>
        /// <param name="filter">This will filter the query based on any property that is not null</param>
        /// <returns>Returns the list of ProjectSourceConfigs found, or a 0 length list if none were found</returns>
        IList<ProjectSourceTypeObj> GetProjectSourceTypes(ProjectSourceTypeFilter filter);

        /// <summary>
        /// Adds a new entry to the ProjectSourceConfig table
        /// </summary>
        /// <param name="obj">The item to add</param>
        /// <returns>The Id of the created item or -1 if no item was created</returns>
        int CreateProjectSourceType(ProjectSourceTypeObj obj, string username);

        /// <summary>
        /// Updates an entry in the ProjectSourceConfig table
        /// </summary>
        /// <param name="obj">The item to update. It will be updated based on it's Id</param>
        /// <returns>True if the Id was found and the entry updated, or false if the Id was not found or the entry not able to be updated</returns>
        bool UpdateProjectSourceType(ProjectSourceTypeObj obj, string username);
        #endregion

        #region -ProjectSource
        /// <summary>
        /// Returns a list of objects from the ProjectSource table
        /// </summary>
        /// <param name="filter">This will filter the query based on any property that is not null</param>
        /// <returns>Returns the list of ProjectSources found, or a 0 length list if none were found</returns>
        IList<ProjectSourceObj> GetProjectSources(ProjectSourceFilter filter);

        /// <summary>
        /// Adds a new entry to the ProjectSource table
        /// </summary>
        /// <param name="obj">The item to add</param>
        /// <returns>The Id of the created item or -1 if no item was created</returns>
        int CreateProjectSource(ProjectSourceObj obj, string username);

        /// <summary>
        /// Updates an entry in the ProjectSource table
        /// </summary>
        /// <param name="obj">The item to update. It will be updated based on it's Id</param>
        /// <returns>True if the Id was found and the entry updated, or false if the Id was not found or the entry not able to be updated</returns>
        bool UpdateProjectSource(ProjectSourceObj obj, string username);
        #endregion
    }
}
