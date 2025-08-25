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
    public class AssignTechnicianHandler : IRequestHandler<AssignTechnicianCommand, MainTaskDTO>
    {
        private readonly IMapper _mapper;
        private readonly IMainTaskRepo _taskRepo;
        private readonly ITaskHub _taskHub;

        public AssignTechnicianHandler(IMapper mapper, IMainTaskRepo taskRepo, ITaskHub taskHub)
        {
            _mapper = mapper;
            _taskRepo = taskRepo;
            _taskHub = taskHub;
        }

        public async Task<MainTaskDTO> Handle(AssignTechnicianCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepo.GetTaskById(request.TaskId);

            if (task.TechnicianId != null)
            {
                var dtoOld = _mapper.Map<MainTaskDTO>(task);
                await _taskHub.SendRemoveAssignTaskToClient(dtoOld, task.TechnicianId);
            }

            task.TechnicianId = request.TechId; 

            await _taskRepo.UpdateTask();
            var updatedTask = await _taskRepo.GetTaskById(request.TaskId);

            var dto = _mapper.Map<MainTaskDTO>(updatedTask);
            await _taskHub.SendUpdatedAssignTaskToClient(dto, request.TechId);

            return dto;
        }
    }
}
