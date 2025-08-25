using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Commands
{
    public class EditScheduleTaskInScheduleCommand : IRequest<DisplayMaintenanceScheduleDTO>
    {
        public int ScheduleId { get; set; }
        public ScheduleTaskDTO Task { get; set; }
        public EditScheduleTaskInScheduleCommand(int scheduleId, ScheduleTaskDTO task)
        {
            ScheduleId = scheduleId;
            Task = task;
        }
    }
}
