using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace InnerTrack.Common.Objs
{
    public class EventObj : DbObject
    {
        [Required]
        public int SourceEventId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Message { get; set; }
    }
}
