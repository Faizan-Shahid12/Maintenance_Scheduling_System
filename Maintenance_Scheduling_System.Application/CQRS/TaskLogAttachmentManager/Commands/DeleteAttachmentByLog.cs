using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Commands
{
    public class DeleteAttachmentsByLogCommand : IRequest
    {
        public int LogId { get; set; }
        public DeleteAttachmentsByLogCommand(int logId)
        {
            LogId = logId;
        }
    }
}
