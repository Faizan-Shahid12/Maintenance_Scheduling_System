using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskLogManager.Commands
{
    public class DeleteTaskLogCommand : IRequest<TaskLogDTO>
    {
        public int LogId { get; set; }

        public DeleteTaskLogCommand(int logId)
        {
            LogId = logId;
        }
    }
}
