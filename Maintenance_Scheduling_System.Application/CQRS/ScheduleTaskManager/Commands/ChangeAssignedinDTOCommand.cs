using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands
{
    public class ChangeAssignedInDTOCommand : IRequest
    {
        public ScheduleTaskDTO TaskDTO { get; }
        public ScheduleTask ScheduleTask { get; }

        public ChangeAssignedInDTOCommand(ScheduleTaskDTO taskDTO, ScheduleTask scheduleTask)
        {
            TaskDTO = taskDTO;
            ScheduleTask = scheduleTask;
        }
    }
}
