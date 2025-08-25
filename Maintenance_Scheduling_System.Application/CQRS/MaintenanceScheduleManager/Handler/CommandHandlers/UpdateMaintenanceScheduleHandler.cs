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
    public class UpdateMaintenanceScheduleHandler : IRequestHandler<UpdateMaintenanceScheduleCommand, DisplayMaintenanceScheduleDTO>
    {
        private readonly IMaintenanceScheduleRepo _repo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateMaintenanceScheduleHandler(IMaintenanceScheduleRepo repo,IMapper mapper,IMediator mediator)
        {
            _repo = repo;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<DisplayMaintenanceScheduleDTO> Handle( UpdateMaintenanceScheduleCommand request,CancellationToken cancellationToken)
        {
            var schedule = await _repo.GetMaintenanceScheduleById(request.MaintenanceSchedule.ScheduleId)
                          ?? throw new Exception("Schedule not found");

            if (schedule.StartDate != request.MaintenanceSchedule.StartDate)
            {
                schedule.StartDate = request.MaintenanceSchedule.StartDate;
                await _mediator.Send(new ChangeDueDateCommand(schedule.StartDate,schedule.ScheduleTasks));
            }

            schedule.ScheduleName = request.MaintenanceSchedule.ScheduleName;
            schedule.ScheduleType = request.MaintenanceSchedule.ScheduleType;
            schedule.EndDate = request.MaintenanceSchedule.EndDate;
            schedule.IsActive = request.MaintenanceSchedule.IsActive;
            schedule.Interval = request.MaintenanceSchedule.Interval;

            await _repo.UpdateMaintenanceSchedule();

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
