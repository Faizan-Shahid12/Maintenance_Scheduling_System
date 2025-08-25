using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Queries
{
    public class GetAllAttachmentsQuery : IRequest<List<TaskLogAttachment>>
    {
        public int TaskLogId { get; set; }

        public GetAllAttachmentsQuery(int LogId)
        {
            TaskLogId = LogId;
        }
    }
}
