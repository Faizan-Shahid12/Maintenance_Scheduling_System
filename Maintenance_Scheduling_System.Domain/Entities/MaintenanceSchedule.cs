using Maintenance_Scheduling_System.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.Entities
{
    public class MaintenanceSchedule : IAudit
    {
        public int ScheduleId { get; set; }
        public string ScheduleName { get; set; } = string.Empty;
        public string ScheduleType { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public List<ScheduleTask> ScheduleTasks { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public TimeSpan Interval { get; set; }
        public DateOnly LastGeneratedDate { get; set; }

        public int EquipmentId { get; set; }
        public Equipment equipment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastModifiedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public string LastModifiedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
