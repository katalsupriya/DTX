using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismDXP.Core
{
      /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract class BaseEntity : ISoftDelete
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [Key]
        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Is transient
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Result</returns>
        private static bool IsTransient(BaseEntity obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }
        protected BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }
    }
}
