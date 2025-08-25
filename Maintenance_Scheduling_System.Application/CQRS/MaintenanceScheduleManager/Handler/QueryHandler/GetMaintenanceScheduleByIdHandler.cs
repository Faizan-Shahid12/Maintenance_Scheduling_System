using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Queries;
using Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Handler.QueryHandler
{
    public class GetMaintenanceScheduleByIdHandler : IRequestHandler<GetMaintenanceScheduleByIdQuery, DisplayMaintenanceScheduleDTO?>
    {
        private readonly IMaintenanceScheduleRepo _repo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetMaintenanceScheduleByIdHandler(IMaintenanceScheduleRepo repo,IMapper mapper,IMediator mediator)
        {
            _repo = repo;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<DisplayMaintenanceScheduleDTO?> Handle(GetMaintenanceScheduleByIdQuery request,CancellationToken cancellationToken)
        {
            var schedule = await _repo.GetMaintenanceScheduleById(request.ScheduleId);
            if (schedule == null) return null;

            var dto = _mapper.Map<DisplayMaintenanceScheduleDTO>(schedule);
            var dtoTasks = new List<ScheduleTaskDTO>();

            foreach (var task in schedule.ScheduleTasks)
            {
                var taskDto = _mapper.Map<ScheduleTaskDTO>(task);
                await _mediator.Send(new ChangeAssignedInDTOCommand(taskDto, task));
                dtoTasks.Add(taskDto);
            }

            dto.ScheduleTasks = dtoTasks;
            return dto;
        }
    }
}

