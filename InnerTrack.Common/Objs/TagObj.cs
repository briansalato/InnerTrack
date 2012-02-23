using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace InnerTrack.Common.Objs
{
    public class TagObj : DbObject
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
