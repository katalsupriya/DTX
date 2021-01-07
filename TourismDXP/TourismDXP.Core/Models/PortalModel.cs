using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismDXP.Core.Models
{
    public class PortalModel: BaseModel
    {

        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string ConnectionString { get; set; }
    }
}
