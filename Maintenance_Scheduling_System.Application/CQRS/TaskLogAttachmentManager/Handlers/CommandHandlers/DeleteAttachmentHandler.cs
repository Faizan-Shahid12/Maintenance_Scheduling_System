using Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Commands;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Handlers.CommandHandlers
{
    public class DeleteAttachmentHandler : IRequestHandler<DeleteAttachmentCommand, TaskLogAttachment>
    {
        private readonly ITaskLogAttachmentsRepo _attachmentRepo;

        public DeleteAttachmentHandler(ITaskLogAttachmentsRepo attachmentRepo)
        {
            _attachmentRepo = attachmentRepo;
        }

        public async Task<TaskLogAttachment> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
        {
            var attach = await _attachmentRepo.GetTaskLogAttachmentById(request.AttachmentId);

            if (attach == null)
                throw new Exception("Attachment not found.");

            attach.IsDeleted = true;

            await _attachmentRepo.DeleteTaskLogAttachment();
            return attach;
        }
    }
}
