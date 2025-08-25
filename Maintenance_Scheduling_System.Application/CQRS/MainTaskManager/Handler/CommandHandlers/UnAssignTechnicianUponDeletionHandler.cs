using Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Handler.CommandHandlers
{
    public class UnAssignTechnicianTaskUponDeletionHandler: IRequestHandler<UnAssignTechnicianTaskUponDeletionCommand>
    {
        private readonly IMainTaskRepo _taskRepo;

        public UnAssignTechnicianTaskUponDeletionHandler(IMainTaskRepo taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public async Task Handle(UnAssignTechnicianTaskUponDeletionCommand request, CancellationToken cancellationToken)
        {
            await _taskRepo.UnAssignTechnicianTask(request.TechId);
            return ;
        }
    }
}
