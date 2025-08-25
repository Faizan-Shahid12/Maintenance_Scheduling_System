using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MainTaskLogManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Commands;
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
    public class DeleteTaskLogHandler : IRequestHandler<DeleteTaskLogCommand, TaskLogDTO>
    {
        private readonly ITaskLogRepo _taskLogRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public DeleteTaskLogHandler(ITaskLogRepo taskLogRepo, IMapper mapper, IMediator mediator)
        {
            _taskLogRepo = taskLogRepo;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<TaskLogDTO> Handle(DeleteTaskLogCommand request, CancellationToken cancellationToken)
        {
            var log = await _taskLogRepo.GetTaskLogByLogId(request.LogId);
            if (log == null) throw new Exception("Task log not found");

            log.IsDeleted = true;

            await _taskLogRepo.DeleteTaskLogs(log);
            await _mediator.Send(new DeleteAttachmentsByLogCommand(log.LogId));

            return _mapper.Map<TaskLogDTO>(log);
        }

    }
}
