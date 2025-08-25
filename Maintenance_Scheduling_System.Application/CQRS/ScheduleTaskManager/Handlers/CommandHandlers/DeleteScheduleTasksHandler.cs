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
    public class DeleteScheduleTasksHandler : IRequestHandler<DeleteScheduleTasksCommand>
    {
        private readonly IScheduleTaskRepo _repo;

        public DeleteScheduleTasksHandler(IScheduleTaskRepo repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteScheduleTasksCommand request, CancellationToken cancellationToken)
        {
            foreach (var scheduleTask in request.ScheduleTasks.ToList())
            {
                scheduleTask.IsDeleted = true;
            }

            await _repo.DeleteScheduleTask();

            return ;
        }
    }
}
