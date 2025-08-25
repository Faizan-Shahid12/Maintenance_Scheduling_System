using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Commands
{
    public class UploadAttachmentCommand : IRequest<TaskLogAttachment>
    {
        public int LogId { get; set; }
        public IFormFile File { get; set; }

        public UploadAttachmentCommand(int logId, IFormFile file)
        {
            LogId = logId;
            File = file;
        }
    }
}
