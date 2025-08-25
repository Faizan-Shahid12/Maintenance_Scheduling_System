using Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Handlers.CommandHandlers
{
    public class ChangeDueDateHandler : IRequestHandler<ChangeDueDateCommand>
    {
        private readonly IScheduleTaskRepo _repo;

        public ChangeDueDateHandler(IScheduleTaskRepo repo)
        {
            _repo = repo;
        }

        public async Task Handle(ChangeDueDateCommand request, CancellationToken cancellationToken)
        {
            foreach (var task in request.ScheduleTasks.ToList())
            {
                task.DueDate = request.ScheduleDate.AddDays((int)task.Interval.TotalDays);
                await _repo.UpdateScheduleTask(task);
            }
            return;
        }
    }
}
