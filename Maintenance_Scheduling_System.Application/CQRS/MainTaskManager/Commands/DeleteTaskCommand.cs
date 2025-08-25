using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands
{
    public class DeleteTaskCommand : IRequest<MainTaskDTO>
    {
        public int TaskId { get; set; }
        public DeleteTaskCommand(int taskId) => TaskId = taskId;
    }
}
