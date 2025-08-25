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
    public class ChangeTaskStatusHandler : IRequestHandler<ChangeTaskStatusCommand, MainTaskDTO>
    {
        private readonly IMapper _mapper;
        private readonly IMainTaskRepo _taskRepo;
        private readonly ITaskHub _taskHub;
        private readonly ICurrentUser _currentUser;

        public ChangeTaskStatusHandler(IMapper mapper, IMainTaskRepo taskRepo, ICurrentUser currentUser, ITaskHub taskHub)
        {
            _mapper = mapper;
            _taskRepo = taskRepo;
            _taskHub = taskHub;
            _currentUser = currentUser;
        }

        public async Task<MainTaskDTO> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepo.GetTaskById(request.TaskId);
            task.Status = request.Status;

            await _taskRepo.UpdateTask();

            var dto = _mapper.Map<MainTaskDTO>(task);
            await _taskHub.SendChangeTaskStatusToClient(dto, task.TechnicianId, _currentUser.Role);

            return dto;
        }
    }
}
