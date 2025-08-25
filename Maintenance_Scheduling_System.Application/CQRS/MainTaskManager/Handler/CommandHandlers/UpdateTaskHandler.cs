using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Application.HubInterfaces;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Handler.CommandHandlers
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, MainTaskDTO>
    {
        private readonly IMapper _mapper;
        private readonly IMainTaskRepo _taskRepo;
        private readonly ITaskHub _taskHub;

        public UpdateTaskHandler(IMapper mapper, IMainTaskRepo taskRepo, ICurrentUser currentUser, ITaskHub taskHub)
        {
            _mapper = mapper;
            _taskRepo = taskRepo;
            _taskHub = taskHub;
        }

        public async Task<MainTaskDTO> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepo.GetTaskById(request.TaskDto.TaskId);

            task.TaskName = request.TaskDto.TaskName;
            task.EquipmentName = request.TaskDto.EquipmentName;
            task.DueDate = request.TaskDto.DueDate;
            task.Priority = request.TaskDto.Priority;
            task.Status = request.TaskDto.Status;

            await _taskRepo.UpdateTask();

            var dto = _mapper.Map<MainTaskDTO>(task);
            await _taskHub.SendEditedTaskToClient(dto, task.TechnicianId);

            return dto;
        }
    }

}
