using Maintenance_Scheduling_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs
{
    public class MainTaskDTO
    {
        public int TaskId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string TaskName { get; set; } = string.Empty;
        public PriorityEnum Priority { get; set; } = PriorityEnum.Low;
        public StatusEnum Status { get; set; } = StatusEnum.Pending;
        public DateOnly DueDate { get; set; }
    }
}
