using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Handler.CommandHandlers
{
    public class DeleteMaintenanceScheduleHandler: IRequestHandler<DeleteMaintenanceScheduleCommand, DisplayMaintenanceScheduleDTO>
    {
        private readonly IMaintenanceScheduleRepo _repo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteMaintenanceScheduleHandler(IMaintenanceScheduleRepo repo,IMapper mapper,IMediator mediator)
        {
            _repo = repo;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<DisplayMaintenanceScheduleDTO> Handle(DeleteMaintenanceScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _repo.GetMaintenanceScheduleById(request.ScheduleId)
                          ?? throw new Exception("Schedule not found");

            await _mediator.Send(new DeleteScheduleTasksCommand(schedule.ScheduleTasks));

            schedule.IsDeleted = true;
            await _repo.DeleteMaintenanceSchedule();

            return _mapper.Map<DisplayMaintenanceScheduleDTO>(schedule);
        }

    }
}
