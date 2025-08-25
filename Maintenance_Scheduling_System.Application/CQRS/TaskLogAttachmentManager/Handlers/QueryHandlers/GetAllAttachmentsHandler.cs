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
    public class GetAllAttachmentsHandler : IRequestHandler<GetAllAttachmentsQuery, List<TaskLogAttachment>>
    {
        private readonly ITaskLogAttachmentsRepo _attachmentRepo;

        public GetAllAttachmentsHandler(ITaskLogAttachmentsRepo attachmentRepo)
        {
            _attachmentRepo = attachmentRepo;
        }

        public async Task<List<TaskLogAttachment>> Handle(GetAllAttachmentsQuery request, CancellationToken cancellationToken)
        {
            return await _attachmentRepo.GetAllTaskLogAttachmentByTaskLogId(request.TaskLogId);
        }
    }
}
