using System.ComponentModel.DataAnnotations.Schema;

namespace TourismDXP.Core.DataModels.Portal
{
    [Table("Portals")]
    public class Portal : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string ConnectionString { get; set; }
    }
}
