using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace InnerTrack.Common.Objs
{
    public class ProjectObj : DbObject
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        public virtual IList<TagObj> Tags { get; set; }
        public virtual IList<UserObj> Members { get; set; }
        public virtual IList<FeedObj> Feeds { get; set; }
        public virtual IList<UserObj> Users { get; set; }
    }
}
