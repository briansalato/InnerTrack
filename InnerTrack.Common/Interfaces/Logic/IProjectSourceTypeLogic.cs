using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Objs;

namespace InnerTrack.Common.Interfaces.Logic
{
    public interface IProjectSourceTypeLogic
    {
        /// <summary>
        /// Returns a single ProjectSourceConfig
        /// </summary>
        /// <param name="id">The id of the ProjectSourceConfig to find</param>
        /// <returns>A ProjectSourceConfig or null if no ProjectSourceConfig has that Id</returns>
        ProjectSourceTypeObj Get(int id);

        /// <summary>
        /// Adds a new entry to the ProjectSourceConfig table
        /// </summary>
        /// <param name="obj">The item to add</param>
        /// <param name="username">The username of who is taking this action</param>
        /// <returns>
        /// The Id of the created item or -1 if no item was created
        /// </returns>
        int Create(ProjectSourceTypeObj obj, string username);

        /// <summary>
        /// Updates an entry in the ProjectSourceConfig table
        /// </summary>
        /// <param name="obj">The item to update. It will be updated based on it's Id</param>
        /// <param name="username">The username of who is taking this action</param>
        /// <returns>True if the Id was found and the entry updated, or false if the Id was not found or the entry not able to be updated</returns>
        bool Update(ProjectSourceTypeObj obj, string username);

        /// <summary>
        /// Get all ProjectSourceConfigs in the repository
        /// </summary>
        /// <returns>This will return a blank list if no ProjectSourceConfigs are found, or the list of ProjectSourceConfigs if there are some</returns>
        IList<ProjectSourceTypeObj> GetAll();
    }
}