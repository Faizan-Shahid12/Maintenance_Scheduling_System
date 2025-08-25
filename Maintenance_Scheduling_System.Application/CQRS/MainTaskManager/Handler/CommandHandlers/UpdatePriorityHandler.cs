using Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Handler.CommandHandlers
{
    public class UpdatePriorityHandler : IRequestHandler<UpdatePriorityCommand>
    {
        private readonly IMainTaskRepo _taskRepo;

        public UpdatePriorityHandler(IMainTaskRepo taskRepo, ICurrentUser currentUser)
        {
            _taskRepo = taskRepo;
        }

        public async Task Handle(UpdatePriorityCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepo.GetTaskById(request.TaskId);
            task.Priority = request.Priority;

            await _taskRepo.UpdateTask();

            return;
        }

    }

}
