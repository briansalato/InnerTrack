using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerTrack.Common.Objs
{
    public class EventObj : IdObject
    {
        public int SourceEventId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
