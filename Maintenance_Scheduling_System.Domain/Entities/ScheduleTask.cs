using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.Entities
{
    public class ScheduleTask
    {
        public int ScheduleTaskId { get; set; }

        public string TaskName { get; set; } = string.Empty;

        public int ScheduleId { get; set; }
        public MaintenanceSchedule schedule { get; set; }

    }
}
