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
    public class GetAllMaintenanceSchedulesByStartDateHandler : IRequestHandler<GetAllMaintenanceSchedulesByStartDateQuery, List<DisplayMaintenanceScheduleDTO>>
    {
        private readonly IMaintenanceScheduleRepo _repo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetAllMaintenanceSchedulesByStartDateHandler(IMaintenanceScheduleRepo repo,IMapper mapper,IMediator mediator)
        {
            _repo = repo;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<DisplayMaintenanceScheduleDTO>> Handle(GetAllMaintenanceSchedulesByStartDateQuery request,CancellationToken cancellationToken)
        {
            var schedules = await _repo.GetAllMaintenanceSchedule();
            var scheduleDTOs = _mapper.Map<List<DisplayMaintenanceScheduleDTO>>(schedules);

            for (int i = 0; i < schedules.Count; i++)
            {
                var dtoTasks = schedules[i].ScheduleTasks
                    .Select(task =>
                    {
                        var dto = _mapper.Map<ScheduleTaskDTO>(task);
                        _mediator.Send(new ChangeAssignedInDTOCommand(dto, task));
                        return dto;
                    })
                    .ToList();

                scheduleDTOs[i].ScheduleTasks = dtoTasks;
            }

            return scheduleDTOs.OrderBy(x => x.StartDate).ToList();
        }
    }

}
