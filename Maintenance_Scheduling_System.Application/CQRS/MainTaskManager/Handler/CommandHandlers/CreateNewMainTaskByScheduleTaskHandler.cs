using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Commands;
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
    public class CreateNewMainTaskByScheduleTaskHandler: IRequestHandler<CreateNewMainTaskByScheduleTaskCommand, List<MainTaskDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IEquipmentRepo _equipmentRepo;
        private readonly IMainTaskRepo _taskRepo;
        private readonly IMediator _mediator;
        private readonly ITaskHub _taskHub;

        public CreateNewMainTaskByScheduleTaskHandler(
            IMapper mapper,
            IEquipmentRepo equipmentRepo,
            IMainTaskRepo taskRepo,
            IMediator mediator,
            ITaskHub taskHub
        )
        {
            _mapper = mapper;
            _equipmentRepo = equipmentRepo;
            _taskRepo = taskRepo;
            _mediator = mediator;
            _taskHub = taskHub;
        }

        public async Task<List<MainTaskDTO>> Handle(CreateNewMainTaskByScheduleTaskCommand request, CancellationToken cancellationToken)
        {
            List<MainTaskDTO> result = new();

            foreach (var scheduleTask in request.ScheduleTasks)
            {
                var mainTask = new MainTask
                {
                    EquipmentName = scheduleTask.EquipmentName,
                    TaskName = scheduleTask.TaskName,
                    DueDate = scheduleTask.DueDate,
                    Priority = scheduleTask.Priority,
                    TechnicianId = scheduleTask.TechnicianId
                };

                var histories = await _mediator.Send( new GetMaintenanceHistoryByEquipmentIdQuery(request.EquipId));
                mainTask.EquipmentId = request.EquipId;

                bool exists = false;
                foreach (var history in histories)
                {
                    if (history.StartDate == DateOnly.FromDateTime(DateTime.Now))
                    {
                        await _mediator.Send(new AddNewTaskToHistoryCommand(history.HistoryId, mainTask));
                        exists = true;
                    }
                }

                var task = mainTask;
                if (!exists)
                {
                    task = await _taskRepo.CreateNewTask(mainTask);
                    var equip = await _equipmentRepo.GetEquipmentById(request.EquipId);

                    var historyDto = new CreateMaintenanceHistoryDTO
                    {
                        EquipmentId = request.EquipId,
                        EquipmentName = equip.Name,
                        EquipmentType = equip.Type,
                        StartDate = DateOnly.FromDateTime(DateTime.Now)
                    };
                    
                    await _mediator.Send(new CreateMaintenanceHistoryFromTaskCommand(historyDto, mainTask,equip));
                    equip.AddMainTask(mainTask);
                }

                if (mainTask.TechnicianId != null)
                    await _taskRepo.LoadTechnician(mainTask);

                var dto = _mapper.Map<MainTaskDTO>(task);
                result.Add(dto);

                await _taskHub.SendTaskToClient(dto, task.TechnicianId);
            }

            return result;
        }
    }
}
