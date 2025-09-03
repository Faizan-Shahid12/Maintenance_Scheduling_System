using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs
{
    public class EquipmentDTO
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string location { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public bool isArchived { get; set; }
        public string WorkShopName { get; set; } = string.Empty;
        public string WorkShopLocation { get; set; } = string.Empty ;
        public double Longitude { get; set; } = 0.0;
        public double Latitude { get; set; } = 0.0;
    }
}
