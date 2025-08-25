using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands
{
    public class DeleteScheduleTaskCommand : IRequest
    {
        public ScheduleTask ScheduleTask { get; }

        public DeleteScheduleTaskCommand(ScheduleTask scheduleTasks)
        {
            ScheduleTask = scheduleTasks;
        }
    }
}
