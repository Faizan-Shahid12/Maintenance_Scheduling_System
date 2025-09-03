using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.DTO.BarCodeDTOs
{
    public class BarCodeResultDTO
    {
        public string EquipmentModel { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public Byte[] BarCode { get; set; } = Array.Empty<Byte>();
    }
}
