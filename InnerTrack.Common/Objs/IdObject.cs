using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace InnerTrack.Common.Objs
{
    public abstract class DbObject
    {
        #region -Properties
        [Key]
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        #endregion

        #region -Methods
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var castedObj = obj as DbObject;

            return Id.HasValue
                   && Id == castedObj.Id;
        }

        public override int GetHashCode()
        {
            //if the Id does not have a value then use the item's hash
            return Id.HasValue ? GetType().GetHashCode() + Id.Value : base.GetHashCode();
        }

        public static bool operator ==(DbObject a, DbObject b)
        {    // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Id.HasValue
                   && a.Id == b.Id;
        }

        public static bool operator !=(DbObject a, DbObject b)
        {
            return !(a == b);
        }
        #endregion
    }
}
