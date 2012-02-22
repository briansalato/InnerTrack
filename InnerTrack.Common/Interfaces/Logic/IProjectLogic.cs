using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Objs;

namespace InnerTrack.Common.Interfaces.Logic
{
    public interface IProjectLogic
    {
        /// <summary>
        /// Returns a single project
        /// </summary>
        /// <param name="id">The id of the project to find</param>
        /// <returns>A project or null if no project has that Id</returns>
        ProjectObj Get(int id);

        /// <summary>
        /// Returns all projects owned by a user
        /// </summary>
        /// <param name="owner">The username to find projects for</param>
        /// <returns>This will return a blank list if no projects are found, or the list of projects if there are some</returns>
        IList<ProjectObj> GetByOwner(string username);

        /// <summary>
        /// Adds a new entry to the project table
        /// </summary>
        /// <param name="proj">The item to add</param>
        /// <param name="username">The username of who is taking this action</param>
        /// <returns>
        /// The Id of the created item or -1 if no item was created
        /// </returns>
        int Create(ProjectObj proj, string username);

        /// <summary>
        /// Updates an entry in the project table
        /// </summary>
        /// <param name="proj">The item to update. It will be updated based on it's Id</param>
        /// <param name="username">The username of who is taking this action</param>
        /// <returns>True if the Id was found and the entry updated, or false if the Id was not found or the entry not able to be updated</returns>
        bool Update(ProjectObj proj, string username);

        /// <summary>
        /// Get all projects in the repository
        /// </summary>
        /// <returns>This will return a blank list if no projects are found, or the list of projects if there are some</returns>
        IList<ProjectObj> GetAll();

        /// <summary>
        /// Returns the search data for the query
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="curPage">The cur page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        SearchResult<ProjectObj> Search(string query, int curPage, int pageSize);
    }
}