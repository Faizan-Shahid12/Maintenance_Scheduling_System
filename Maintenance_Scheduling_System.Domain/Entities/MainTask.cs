using Maintenance_Scheduling_System.Domain.Enums;
using Maintenance_Scheduling_System.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.Entities
{
    public class MainTask : IAudit
    {
        public int TaskId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string TaskName { get; set; } = string.Empty;
        public PriorityEnum Priority { get; set; } = PriorityEnum.Low;
        public StatusEnum Status { get; set; } = StatusEnum.Pending;
        public DateOnly DueDate {  get; set; }

        public int? HistoryId { get; set; }
        public MaintenanceHistory? History { get; set; }

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        public string? TechnicianId { get; set; }
        public AppUser? Technician { get; set; }

        public List<TaskLogs> Logs { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastModifiedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public string LastModifiedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

    }
}
