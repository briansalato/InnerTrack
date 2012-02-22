using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerTrack.Common.Objs
{
    public class ProjectObj : IdObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<TagObj> Tags { get; set; }
        public IList<UserObj> Members { get; set; }
        public IList<FeedObj> Feeds { get; set; }
    }
}
