using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs
{
    public class DisplayMaintenanceScheduleDTO
    {
        public int ScheduleId { get; set; }
        public string ScheduleName { get; set; } = string.Empty;
        public string ScheduleType { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public List<ScheduleTaskDTO> ScheduleTasks { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public TimeSpan Interval { get; set; }
    }
}
