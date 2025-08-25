using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands
{
    public class UpdateTaskCommand : IRequest<MainTaskDTO>
    {
        public MainTaskDTO TaskDto { get; set; }
        public UpdateTaskCommand(MainTaskDTO taskDto) => TaskDto = taskDto;
    }
}
