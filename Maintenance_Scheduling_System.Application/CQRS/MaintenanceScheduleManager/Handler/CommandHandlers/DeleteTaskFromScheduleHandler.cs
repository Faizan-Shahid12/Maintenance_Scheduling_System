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
    public class DeleteTaskFromScheduleHandler : IRequestHandler<DeleteTaskFromScheduleCommand, DisplayMaintenanceScheduleDTO>
    {
        private readonly IMaintenanceScheduleRepo _repo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteTaskFromScheduleHandler(IMaintenanceScheduleRepo repo,IMapper mapper,IMediator mediator)
        {
            _repo = repo;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<DisplayMaintenanceScheduleDTO> Handle( DeleteTaskFromScheduleCommand request,CancellationToken cancellationToken)
        {
            var schedule = await _repo.GetMaintenanceScheduleById(request.ScheduleId)
                          ?? throw new Exception("Schedule not found");

            var task = schedule.ScheduleTasks.FirstOrDefault(t => t.ScheduleTaskId == request.TaskId)
                       ?? throw new Exception("Task not found");

            await _mediator.Send(new DeleteScheduleTaskCommand(task));

            schedule.ScheduleTasks.Remove(task);

            var dto = _mapper.Map<DisplayMaintenanceScheduleDTO>(schedule);
            await ChangeInAssigned(schedule, dto);
            return dto;
        }

        private async Task ChangeInAssigned(MaintenanceSchedule schedule, DisplayMaintenanceScheduleDTO dto)
        {
            for (int i = 0; i < dto.ScheduleTasks.Count; i++)
                await _mediator.Send(new ChangeAssignedInDTOCommand(dto.ScheduleTasks[i], schedule.ScheduleTasks[i]));
        }
    }
}
