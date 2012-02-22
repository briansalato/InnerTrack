using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerTrack.Common.Objs
{
    public class SearchResult<T>
    {
        public bool HasPrevious { get; set; }
        public bool HasMore { get; set; }
        public IList<T> Results { get; set; }
    }
}
