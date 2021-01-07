using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismDXP.Core.Models
{
    public class PortalUserModel : BaseModel
    {
        public int PortalId { get; set; }
        //User Id For Primary Portal
        public int PrimaryUserId { get; set; }
        //this will be userId for secondary portal like e-lerning Database UserId 
        public string SecondaryUserId { get; set; }
    }
}
