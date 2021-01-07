using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismDXP.Core.DataModels
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }
        public string RoleName { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsSeeded { get; set; }

        //from which Role it's been derived 
        public int? ParentId { get; set; }
        //ReadOnly/Read Write
        public int? AccessLevel { get; set; }
        public int? OwnerId { get; set; }

        //categoryId will be used for tracking the subCategoryRole
        public int? CategoryId { get; set; }
    }
}
