using System;
namespace TourismDXP.Core.Models
{
    public abstract class BaseModel : ISoftDelete
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary> 
        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        protected BaseModel()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
