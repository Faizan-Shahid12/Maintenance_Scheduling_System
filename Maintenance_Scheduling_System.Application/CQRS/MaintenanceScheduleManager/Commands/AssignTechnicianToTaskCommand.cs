using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Commands
{
    public class AssignTechnicianToTaskCommand : IRequest<DisplayMaintenanceScheduleDTO>
    {
        public int ScheduleId { get; set; }
        public int TaskId { get; set; }
        public string? TechnicianId { get; set; }
        public AssignTechnicianToTaskCommand(int scheduleId, int taskId, string? technicianId)
        {
            ScheduleId = scheduleId;
            TaskId = taskId;
            TechnicianId = technicianId;
        }
    }
}
