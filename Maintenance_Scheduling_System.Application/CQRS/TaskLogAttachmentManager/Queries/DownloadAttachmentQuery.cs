using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Queries
{
    public class DownloadAttachmentQuery : IRequest<TaskLogAttachment>
    {
        public int AttachmentId { get; set; }

        public DownloadAttachmentQuery(int attachmentId)
        {
            AttachmentId = attachmentId;
        }
    }
}
