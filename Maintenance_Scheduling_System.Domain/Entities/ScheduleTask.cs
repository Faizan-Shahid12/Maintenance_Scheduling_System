using Maintenance_Scheduling_System.Domain.Enums;
using Maintenance_Scheduling_System.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.Entities
{
    public class ScheduleTask : IAudit
    {
        public int ScheduleTaskId { get; set; }

        public string TaskName { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public PriorityEnum Priority { get; set; } = PriorityEnum.Low;
        public DateOnly DueDate { get; set; }
        public TimeSpan Interval { get; set; }
        public string? TechnicianId { get; set; }

        public int ScheduleId { get; set; }
        public MaintenanceSchedule schedule { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastModifiedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public string LastModifiedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

    }
}
