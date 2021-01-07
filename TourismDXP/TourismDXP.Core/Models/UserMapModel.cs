using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismDXP.Core.Models
{
    public class UserMapModel : BaseModel
    {
        //UserId
        public int UserId { get; set; }
        //This Id Is the Client or Admin or Reseller  Table Id  }
        public int UserDetailId { get; set; }
        //Who has Created this 
        public int OwnerId { get; set; }

        //Type of Usr Resseler or Client By Enum
        public int UserType { get; set; }
    }
}
