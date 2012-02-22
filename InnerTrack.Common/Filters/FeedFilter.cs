using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerTrack.Common.Filters
{
    public class FeedFilter : IdFilter
    {
        public int? ProjectId { get; set; }
    }
}
