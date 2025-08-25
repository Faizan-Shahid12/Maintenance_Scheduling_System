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
    public class UpdateTaskLogHandler : IRequestHandler<UpdateTaskLogCommand, TaskLogDTO>
    {
        private readonly ITaskLogRepo _taskLogRepo;
        private readonly IMapper _mapper;

        public UpdateTaskLogHandler(ITaskLogRepo taskLogRepo, IMapper mapper)
        {
            _taskLogRepo = taskLogRepo;
            _mapper = mapper;
        }

        public async Task<TaskLogDTO> Handle(UpdateTaskLogCommand request, CancellationToken cancellationToken)
        {
            var dto = request.TaskLogDTO;
            var log = await _taskLogRepo.GetTaskLogByLogId(dto.LogId);

            if (log == null) throw new Exception("Task log not found");

            log.Note = dto.Note;

            await _taskLogRepo.UpdateTaskLogs(log);

            return _mapper.Map<TaskLogDTO>(log);
        }

    }

}
