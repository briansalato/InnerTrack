using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace InnerTrack.Common.Objs
{
    public class ProjectSourceTypeObj : DbObject
    {
        [Required]
        [StringLength(200)]
        public string FullClassName { get; set; }

        [Required]
        [StringLength(200)]
        public string AssemblyName { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Interval { get; set; }

        public DateTime? LastRun { get; set; }

        [Required]
        public DateTime? NextRun { get; set; }

        [Required]
        public bool Enabled { get; set; }
    }
}
