using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Commands
{
    public class DeleteTaskFromScheduleCommand : IRequest<DisplayMaintenanceScheduleDTO>
    {
        public int ScheduleId { get; set; }
        public int TaskId { get; set; }
        public DeleteTaskFromScheduleCommand(int scheduleId, int taskId)
        {
            ScheduleId = scheduleId;
            TaskId = taskId;
        }
    }
}
