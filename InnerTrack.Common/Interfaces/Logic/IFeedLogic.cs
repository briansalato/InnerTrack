using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Objs;
using InnerTrack.Common.Enumerations;

namespace InnerTrack.Common.Interfaces.Logic
{
    public interface IFeedLogic
    {
        /// <summary>
        /// Returns a single feed
        /// </summary>
        /// <param name="id">The id of the feed to find</param>
        /// <returns>A feed or null if no feed has that Id</returns>
        FeedObj Get(int id);

        /// <summary>
        /// Returns all feeds for a project
        /// </summary>
        /// <param name="projectId">The id of the project to get the feeds of</param>
        /// <returns>A list of feeds if any exist foro that project or an empty list if no project has that id or there are no feeds for that project</returns>
        IList<FeedObj> GetForProject(int projectId);

        /// <summary>
        /// Adds a new entry to the feed table
        /// </summary>
        /// <param name="obj">The item to add</param>
        /// <param name="username">The username of who is taking this action</param>
        /// <returns>
        /// The Id of the created item or -1 if no item was created
        /// </returns>
        int Create(FeedObj obj, string username);

        /// <summary>
        /// Returns all available Feed Types
        /// </summary>
        /// <returns></returns>
        IEnumerable<Common.Enumerations.FeedType> GetFeedTypes();
    }
}
