using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MainTaskLogManager.Queries;
using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskLogManager.Handlers.QueryHandlers
{
    public class GetAllTaskLogsHandler : IRequestHandler<GetAllTaskLogsQuery, List<TaskLogDTO>>
    {
        private readonly ITaskLogRepo _taskLogRepo;
        private readonly IMapper _mapper;

        public GetAllTaskLogsHandler(ITaskLogRepo taskLogRepo, IMapper mapper)
        {
            _taskLogRepo = taskLogRepo;
            _mapper = mapper;
        }

        public async Task<List<TaskLogDTO>> Handle(GetAllTaskLogsQuery request, CancellationToken cancellationToken)
        {
            var logs = await _taskLogRepo.GetAllTaskLogsByTaskId(request.TaskId);
            return _mapper.Map<List<TaskLogDTO>>(logs);
        }
    }
}
