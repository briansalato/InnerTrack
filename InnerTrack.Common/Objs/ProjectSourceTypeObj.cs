using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerTrack.Common.Objs
{
    public class ProjectSourceTypeObj : IdObject
    {
        public string FullClassName { get; set; }
        public string Name { get; set; }
        public int Interval { get; set; }
        public DateTime? LastRun { get; set; }
        public DateTime? NextRun { get; set; }
        public bool Enabled { get; set; }
    }
}
