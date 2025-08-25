using Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Queries;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Handlers.QueryHandlers
{
    public class DownloadAttachmentHandler : IRequestHandler<DownloadAttachmentQuery, TaskLogAttachment>
    {
        private readonly ITaskLogAttachmentsRepo _attachmentRepo;

        public DownloadAttachmentHandler(ITaskLogAttachmentsRepo attachmentRepo)
        {
            _attachmentRepo = attachmentRepo;
        }

        public async Task<TaskLogAttachment> Handle(DownloadAttachmentQuery request, CancellationToken cancellationToken)
        {
            var attachment = await _attachmentRepo.GetTaskLogAttachmentById(request.AttachmentId);

            if (attachment == null || attachment.IsDeleted)
                throw new FileNotFoundException("Attachment not found.");

            return attachment;
        }
    }
}
