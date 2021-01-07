using System.ComponentModel.DataAnnotations.Schema;

namespace TourismDXP.Core.DataModels.Portal
{

    [Table("PortalUsers")]
    public class PortalUser : BaseEntity
    {
        public int PortalId { get; set; }
        //User Id For Primary Portal
        public int PrimaryUserId { get; set; }
        //this will be userId for secondary portal like e-lerning Database UserId 
        public string SecondaryUserId { get; set; }

    }
}
