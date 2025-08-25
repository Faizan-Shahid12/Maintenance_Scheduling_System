using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Queries;
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
    public class AddNewTaskToScheduleHandler: IRequestHandler<AddNewTaskToScheduleCommand, DisplayMaintenanceScheduleDTO>
    {
        private readonly IMaintenanceScheduleRepo _repo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddNewTaskToScheduleHandler(IMaintenanceScheduleRepo repo, IMapper mapper,IMediator mediator)
        {
            _repo = repo;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<DisplayMaintenanceScheduleDTO> Handle(AddNewTaskToScheduleCommand request,CancellationToken cancellationToken)
        {
            var schedule = await _repo.GetMaintenanceScheduleById(request.ScheduleId)
                          ?? throw new Exception("Schedule not found");

            var equipment =  await _mediator.Send(new GetEquipmentByIdQuery(schedule.EquipmentId));

            var newTask = _mapper.Map<ScheduleTask>(request.Task);

            newTask.ScheduleId = schedule.ScheduleId;
            newTask.EquipmentName = equipment.Name;

            schedule.ScheduleTasks.Add(newTask);

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
