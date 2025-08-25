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
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, MainTaskDTO>
    {
        private readonly IMapper _mapper;
        private readonly IMainTaskRepo _taskRepo;
        private readonly ITaskHub _taskHub;

        public DeleteTaskHandler(IMapper mapper, IMainTaskRepo taskRepo, ITaskHub taskhub)
        {
            _mapper = mapper;
            _taskRepo = taskRepo;
            _taskHub = taskhub;
        }

        public async Task<MainTaskDTO> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepo.GetTaskById(request.TaskId);
            task.IsDeleted = true;

            await _taskRepo.DeleteTask();

            var dto = _mapper.Map<MainTaskDTO>(task);

            await _taskHub.SendRemovedTaskToClient(dto,task.TechnicianId);

            return dto;
        }
    }
}
