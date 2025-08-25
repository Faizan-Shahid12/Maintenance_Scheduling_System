using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskLogManager.Commands
{
    public class UpdateTaskLogCommand : IRequest<TaskLogDTO>
    {
        public TaskLogDTO TaskLogDTO { get; set; }

        public UpdateTaskLogCommand(TaskLogDTO dto)
        {
            TaskLogDTO = dto;
        }
    }
}
