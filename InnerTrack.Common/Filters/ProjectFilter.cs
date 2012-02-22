using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerTrack.Common.Filters
{
    public class ProjectFilter : IdFilter
    {
        public string OwnersUserName { get; set; }
        public IList<string> QueryNames { get; set; }
        public IList<string> QueryDescriptions { get; set; }
        public int? StartIndex { get; set; }
        public int? MaxResults { get; set; }
    }
}
