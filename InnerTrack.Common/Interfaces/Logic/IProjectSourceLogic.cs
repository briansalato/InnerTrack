using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Objs;

namespace InnerTrack.Common.Interfaces.Logic
{
    public interface IProjectSourceLogic
    {
        /// <summary>
        /// Returns a single ProjectSource
        /// </summary>
        /// <param name="id">The id of the ProjectSource to find</param>
        /// <returns>A ProjectSource or null if no ProjectSource has that Id</returns>
        ProjectSourceObj Get(int id);

        /// <summary>
        /// Adds a new entry to the ProjectSource table
        /// </summary>
        /// <param name="obj">The item to add</param>
        /// <param name="username">The username of who is taking this action</param>
        /// <returns>
        /// The Id of the created item or -1 if no item was created
        /// </returns>
        int Create(ProjectSourceObj obj, string username);

        /// <summary>
        /// Updates an entry in the ProjectSource table
        /// </summary>
        /// <param name="obj">The item to update. It will be updated based on it's Id</param>
        /// <param name="username">The username of who is taking this action</param>
        /// <returns>True if the Id was found and the entry updated, or false if the Id was not found or the entry not able to be updated</returns>
        bool Update(ProjectSourceObj obj, string username);

        /// <summary>
        /// Get all ProjectSources in the repository
        /// </summary>
        /// <returns>This will return a blank list if no ProjectSources are found, or the list of ProjectSources if there are some</returns>
        IList<ProjectSourceObj> GetAll();
    }
}