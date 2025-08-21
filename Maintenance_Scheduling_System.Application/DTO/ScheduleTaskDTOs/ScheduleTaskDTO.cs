using Maintenance_Scheduling_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs
{
    public class ScheduleTaskDTO
    {
        public int ScheduleTaskId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public PriorityEnum Priority { get; set; } = PriorityEnum.Low;
        public DateOnly DueDate { get; set; }
        public TimeSpan Interval { get; set; }
        public string? AssignedTo { get; set; }
        public string? TechEmail { get; set; }
    }
}
