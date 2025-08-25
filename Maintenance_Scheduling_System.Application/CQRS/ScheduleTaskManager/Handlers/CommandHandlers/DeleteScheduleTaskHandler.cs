using Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Handlers.CommandHandlers
{
    public class DeleteScheduleTaskHandler : IRequestHandler<DeleteScheduleTaskCommand>
    {
        private readonly IScheduleTaskRepo _repo;

        public DeleteScheduleTaskHandler(IScheduleTaskRepo repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteScheduleTaskCommand request, CancellationToken cancellationToken)
        {
            request.ScheduleTask.IsDeleted = true;
            await _repo.DeleteScheduleTask();
            return ;
        }
    }
}
