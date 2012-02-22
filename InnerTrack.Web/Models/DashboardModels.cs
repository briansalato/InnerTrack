using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnerTrack.Web.Models
{
    public class DashboardModel
    {
        public IEnumerable<ListItemProjectStatusModel> ProjectStatuses { get; set; }
        public IEnumerable<ListItemEventModel> FeedEvents { get; set; }
    }
}