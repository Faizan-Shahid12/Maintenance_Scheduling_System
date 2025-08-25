using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands
{
    public class ChangeTaskStatusCommand : IRequest<MainTaskDTO>
    {
        public int TaskId { get; set; }
        public StatusEnum Status { get; set; }
        public ChangeTaskStatusCommand(int taskId, StatusEnum status)
        {
            TaskId = taskId;
            Status = status;
        }
    }
}
