using Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Commands;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Handlers.CommandHandlers
{
    public class DeleteAttachmentsByLogHandler : IRequestHandler<DeleteAttachmentsByLogCommand>
    {
        private readonly ITaskLogAttachmentsRepo _attachmentRepo;

        public DeleteAttachmentsByLogHandler(ITaskLogAttachmentsRepo attachmentRepo)
        {
            _attachmentRepo = attachmentRepo;
        }

        public async Task Handle(DeleteAttachmentsByLogCommand request, CancellationToken cancellationToken)
        {
            var attachments = await _attachmentRepo.GetAllTaskLogAttachmentByTaskLogId(request.LogId);

            foreach (var attach in attachments)
            {
                attach.IsDeleted = true;
            }

            await _attachmentRepo.DeleteTaskLogAttachment();
        }
    }
}
