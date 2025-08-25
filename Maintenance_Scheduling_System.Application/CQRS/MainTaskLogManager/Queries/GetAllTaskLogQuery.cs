using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskLogManager.Queries
{
    public class GetAllTaskLogsQuery : IRequest<List<TaskLogDTO>>
    {
        public int TaskId { get; set; }

        public GetAllTaskLogsQuery(int taskId)
        {
            TaskId = taskId;
        }
    }
}
