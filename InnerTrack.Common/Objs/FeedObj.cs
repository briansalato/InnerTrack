using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InnerTrack.Common.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace InnerTrack.Common.Objs
{
    public class FeedObj : DbObject
    {
        [Required]
        public virtual FeedTypeObj Type { get; set; }
        public virtual IList<EventObj> Events { get; set; }
        public virtual IList<ProjectObj> Projects { get; set; }
    }
}
