using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismDXP.Core.Models
{
    public class UserInRoleModel : BaseModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
