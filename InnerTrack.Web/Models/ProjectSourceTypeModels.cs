using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace InnerTrack.Web.Models
{
    public class ShowProjectSourceTypeModel
    {
        public int Id { get; set; }
        public string AssemblyName { get; set; }
        public string FullClassName { get; set; }
        public string Name { get; set; }
        public int Interval { get; set; }
        public DateTime? LastRun { get; set; }
        public DateTime NextRun { get; set; }
        public bool Enabled { get; set; }
    }

    public class EditProjectSourceTypeModel
    {
        public int? Id { get; set; }
        public string AssemblyName { get; set; }
        public string FullClassName { get; set; }
        public string Name { get; set; }
        public int Interval { get; set; }

        [Description("Force this source to run now?")]
        public bool ForceRun { get; set; }
        public bool Enabled { get; set; }
    }

    public class ListProjectSourceTypeModel
    {
        public IList<ListItemProjectSourceTypeModel> ProjectSourceTypes { get; set; }
    }

    public class ListItemProjectSourceTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime NextRun { get; set; }
        public bool Enabled { get; set; }
    }
}