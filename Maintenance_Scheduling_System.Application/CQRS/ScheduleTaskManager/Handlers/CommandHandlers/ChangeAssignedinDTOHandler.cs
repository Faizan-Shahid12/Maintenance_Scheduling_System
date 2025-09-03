using Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Queries;
using Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Handlers.CommandHandlers
{
    public class ChangeAssignedInDTOHandler : IRequestHandler<ChangeAssignedInDTOCommand>
    {
        private readonly IMediator _mediator;
        public ChangeAssignedInDTOHandler(IMediator mediator)
        { 
           _mediator = mediator;
        }

        public async Task Handle(ChangeAssignedInDTOCommand request, CancellationToken cancellationToken)
        {
            var scheduleTask = request.ScheduleTask;
            var taskDTO = request.TaskDTO;

            if (!string.IsNullOrEmpty(scheduleTask?.TechnicianId))
            {
                var tech = await _mediator.Send(new GetTechnicianByIdQuery(scheduleTask.TechnicianId));

                if (tech != null)
                {
                    taskDTO.AssignedTo = tech.FullName;
                    taskDTO.TechEmail = tech.Email;
                }
                else
                {
                    taskDTO.AssignedTo = "Technician Not Found";
                }
            }
            else
            {
                taskDTO.AssignedTo = "N/A";
            }

            return ;
        }
    }
}

