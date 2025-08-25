using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Handlers.QueryHandlers;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Queries;
using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
using Maintenance_Scheduling_System.Application.HubInterfaces;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Handler.CommandHandlers
{
    public class CreateNewMainTaskHandler : IRequestHandler<CreateNewMainTaskCommand, MainTaskDTO>
    {
        private readonly IMapper _mapper;
        private readonly IEquipmentRepo _equipmentRepo;
        private readonly IMainTaskRepo _taskRepo;
        private readonly IMediator _mediator;
        private readonly ITaskHub _taskHub;

        public CreateNewMainTaskHandler(
            IMapper mapper,
            IEquipmentRepo equipmentRepo,
            IMainTaskRepo taskRepo,
            IMediator mediator,
            ITaskHub taskHub)
        {
            _mapper = mapper;
            _equipmentRepo = equipmentRepo;
            _taskRepo = taskRepo;
            _mediator = mediator;
            _taskHub = taskHub;
        }

        public async Task<MainTaskDTO> Handle(CreateNewMainTaskCommand request, CancellationToken cancellationToken)
        {
            var mainTask = _mapper.Map<MainTask>(request);

            var histories = await _mediator.Send(new GetMaintenanceHistoryByEquipmentIdQuery(request.EquipId));
            var equip = await _equipmentRepo.GetEquipmentById(request.EquipId);

            mainTask.EquipmentId = request.EquipId;
            mainTask.EquipmentName = equip.Name;


            bool check = false;

            foreach (var history in histories)
            {
                if (history.StartDate == DateOnly.FromDateTime(DateTime.Now))
                {
                    await _mediator.Send(new AddNewTaskToHistoryCommand(history.HistoryId, mainTask));
                    check = true;
                }
            }

            var task = mainTask;

            if (!check)
            {
                task = await _taskRepo.CreateNewTask(mainTask);

                var historyDto = new CreateMaintenanceHistoryDTO
                {
                    EquipmentId = request.EquipId,
                    EquipmentName = equip.Name,
                    EquipmentType = equip.Type,
                    StartDate = DateOnly.FromDateTime(DateTime.Now)
                };

                await _mediator.Send(new CreateMaintenanceHistoryFromTaskCommand(historyDto, mainTask, equip));
                equip.AddMainTask(mainTask);
            }

            var dto = _mapper.Map<MainTaskDTO>(task);
            await _taskHub.SendTaskToClient(dto, task.TechnicianId);

            return dto;
        }
    }
}
