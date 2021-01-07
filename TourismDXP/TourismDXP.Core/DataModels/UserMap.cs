using System.ComponentModel.DataAnnotations.Schema;

namespace TourismDXP.Core.DataModels
{
    [Table("UserMap")]
    public class UserMap : BaseEntity
    {
        //UserId
        public int UserId { get; set; }
        //This Id Is the Client or Admin or Reseller  Table Id 
        public int UserDetailId { get; set; }
        public int OwnerId { get; set; }
        public int UserType { get; set; }
    }
}
