using Maintenance_Scheduling_System.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Interfaces
{
    public interface ITaskLogAttachmentService
    {
        Task UploadAttachment(int logId, IFormFile file);
        Task<TaskLogAttachment> DownloadAttachment(int attachId);
        Task DeleteAttachment(int attachId);
        Task DeleteAttachmentByLog(int logId);
    }
}
