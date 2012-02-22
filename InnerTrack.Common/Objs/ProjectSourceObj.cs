using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerTrack.Common.Objs
{
    public class ProjectSourceObj : IdObject
    {
        public string Configuration { get; set; }
        public ProjectSourceTypeObj SourceConfig { get; set; }
    }
}
