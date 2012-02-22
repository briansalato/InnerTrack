using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnerTrack.Common.Enumerations;
using System.Web.Mvc;

namespace InnerTrack.Web.Models
{
    public class ListItemFeedModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class ShowFeedModel
    {
        public int Id { get; set; }
        public FeedType Type { get; set; }
        public IList<ListItemEventModel> Events { get; set; }
    }

    public class EditFeedModel
    {
        public int? Id { get; set; }
        public FeedType Type { get; set; }
        public int ProjectId { get; set; }

        public IEnumerable<SelectListItem> AvailableTypes { get; set; }
    }
}