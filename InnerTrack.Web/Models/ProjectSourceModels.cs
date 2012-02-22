using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

namespace InnerTrack.Web.Models
{
    public class ShowProjectSourceModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Configuration { get; set; }
    }

    public class EditProjectSourceModel
    {
        public int? Id { get; set; }
        public string Configuration { get; set; }

        [Description("Type")]
        public int TypeId { get; set; }
        public SelectList AvailableTypes { get; set; }
    }

    public class ListProjectSourceModel
    {
        public IList<ListItemProjectSourceModel> ProjectSources { get; set; }
    }

    public class ListItemProjectSourceModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Configuration { get; set; }
    }
}