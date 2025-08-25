using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MainTaskLogManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskLogManager.Handlers.CommandHandlers
{
    public class CreateTaskLogHandler : IRequestHandler<CreateTaskLogCommand, TaskLogDTO>
    {
        private readonly ITaskLogRepo _taskLogRepo;
        private readonly IMainTaskRepo _mainTaskRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public CreateTaskLogHandler(IMainTaskRepo mainTaskRepo, ITaskLogRepo taskLogRepo, IMapper mapper, IMainTaskRepo MainTaskRepo)
        {
            _taskLogRepo = taskLogRepo;
            _mapper = mapper;
            _mainTaskRepo = MainTaskRepo;
        }

        public async Task<TaskLogDTO> Handle(CreateTaskLogCommand request, CancellationToken cancellationToken)
        {
            var dto = request.TaskLogDTO;

            var mainTask = await _mainTaskRepo.GetTaskById(dto.TaskId);

            if (mainTask == null) throw new Exception("Main task not found");

            var log = _mapper.Map<TaskLogs>(dto);
            log.TaskId = dto.TaskId;

            await _taskLogRepo.CreateNewTaskLogs(log);

            return _mapper.Map<TaskLogDTO>(log);
        }

       
    }
}
