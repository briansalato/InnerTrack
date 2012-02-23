using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace InnerTrack.Common.Objs
{
    public class ProjectSourceObj : DbObject
    {
        [Required]
        public string Configuration { get; set; }
        public virtual ProjectSourceTypeObj SourceConfig { get; set; }
    }
}
