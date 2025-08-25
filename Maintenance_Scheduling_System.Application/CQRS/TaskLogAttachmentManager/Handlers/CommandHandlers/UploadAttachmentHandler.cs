using Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Commands;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Handlers.CommandHandlers
{
    public class UploadAttachmentCommandHandler : IRequestHandler<UploadAttachmentCommand, TaskLogAttachment>
    {
        private readonly ITaskLogRepo _taskLogRepo;
        private readonly ITaskLogAttachmentsRepo _attachmentRepo;
        private readonly string _webRoot;

        public UploadAttachmentCommandHandler(ITaskLogRepo taskLogRepo,ITaskLogAttachmentsRepo attachmentRepo,IWebHostEnvironment env)
        {
            _taskLogRepo = taskLogRepo;
            _attachmentRepo = attachmentRepo;
            _webRoot = env.WebRootPath;
        }

        public async Task<TaskLogAttachment> Handle(UploadAttachmentCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
                throw new ArgumentException("Invalid file.");

            var log = await _taskLogRepo.GetTaskLogByLogId(request.LogId);
            
            if (log == null)
                throw new Exception("Task log not found.");

            string uploadsFolder = Path.Combine(_webRoot, "Uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = $"{Guid.NewGuid()}_{request.File.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            var attachment = new TaskLogAttachment
            {
                FileName = request.File.FileName,
                FilePath = $"Uploads/{uniqueFileName}",
                ContentType = request.File.ContentType,
                LogId = request.LogId,
            };

            await _attachmentRepo.CreateNewTaskLogAttachment(attachment);
            return attachment;
        }
    }
}
