using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Enumerations;

namespace InnerTrack.Common.Objs
{
    public class FeedObj : IdObject
    {
        public FeedTypeObj Type { get; set; }
        public IList<EventObj> Events { get; set; }
        public IList<ProjectObj> Projects { get; set; }
    }
}
