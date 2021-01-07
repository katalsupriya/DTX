using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismDXP.Core.DataModels
{
    [Table("UserInRoles")]
    public class UserInRole : BaseEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
