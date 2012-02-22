using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnerTrack.Web.Models
{
    public class ShowProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ListItemTagModel> Tags { get; set; }
        public IList<ListItemFeedModel> Feeds { get; set; }
    }

    public class EditProjectModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ListItemTagModel> Tags { get; set; }
    }

    public class ListProjectModel
    {
        public IList<ListItemProjectModel> Projects { get; set; }
    }

    public class ListItemProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ListItemProjectStatusModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}