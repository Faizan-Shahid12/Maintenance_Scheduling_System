using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskLogManager.Commands
{
    public class CreateTaskLogCommand : IRequest<TaskLogDTO>
    {
        public CreateTaskLogDTO TaskLogDTO { get; set; }

        public CreateTaskLogCommand(CreateTaskLogDTO dto)
        {
            TaskLogDTO = dto;
        }
    }
}
