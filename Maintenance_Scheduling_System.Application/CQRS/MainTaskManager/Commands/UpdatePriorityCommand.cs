using Maintenance_Scheduling_System.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands
{
    public class UpdatePriorityCommand : IRequest
    {
        public int TaskId { get; set; }
        public PriorityEnum Priority { get; set; }
        public UpdatePriorityCommand(int taskId, PriorityEnum priority)
        {
            TaskId = taskId;
            Priority = priority;
        }
    }
}
