using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands
{
    public class DeleteScheduleTasksCommand : IRequest
    {
        public List<ScheduleTask> ScheduleTasks { get; }

        public DeleteScheduleTasksCommand(List<ScheduleTask> scheduleTasks)
        {
            ScheduleTasks = scheduleTasks;
        }
    }
}
