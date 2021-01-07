using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismDXP.Core.Models
{
    public class ListItemModel : BaseModel
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
