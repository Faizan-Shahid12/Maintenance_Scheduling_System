using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands
{
    public class ChangeDueDateCommand : IRequest
    {
        public DateOnly ScheduleDate { get; }
        public List<ScheduleTask> ScheduleTasks { get; }

        public ChangeDueDateCommand(DateOnly scheduleDate, List<ScheduleTask> scheduleTasks)
        {
            ScheduleDate = scheduleDate;
            ScheduleTasks = scheduleTasks;
        }
    }
}
