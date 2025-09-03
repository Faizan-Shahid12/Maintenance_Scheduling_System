using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.Entities
{
    public class WorkShopLoc
    {
        public int WorkShopId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public double Longitude { get; set; } = 0.0;
        public double Latitude { get; set; } = 0.0;
        public List<Equipment> equipments { get; set; }

    }
}
