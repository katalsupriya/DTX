using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismDXP.Core.DataModels
{
    [Table("Users")]
    public class User : BaseEntity
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }
        public int? RoleId { get; set; } //will save UserType as roleId for userModule while adding client and reseller
        //public bool? IsDeleted { get; set; }
        public string Role { get; set; }
        public string Company { get; set; }

        public string Username { get; set; }
        public string Code { get; set; }
        public bool IsCodeUsed { get; set; }

        public int? OwnerId { get; set; }
    }
}
