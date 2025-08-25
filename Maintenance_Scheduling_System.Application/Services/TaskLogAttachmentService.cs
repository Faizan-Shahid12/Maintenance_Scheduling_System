using AutoMapper;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Services
{
    public class TaskLogAttachmentService : ITaskLogAttachmentService
    {
        
        public ITaskLogRepo TaskLogRepository { get; set; }
        public ITaskLogAttachmentsRepo LogAttachmentRepository { get; set; }
        public ICurrentUser currentUser { get; set; }

        private readonly string webRoot;

        public TaskLogAttachmentService(ITaskLogRepo logrepo, ITaskLogAttachmentsRepo attachrepo, ICurrentUser user, IWebHostEnvironment env)
        {
            TaskLogRepository = logrepo;
            LogAttachmentRepository = attachrepo;
            currentUser = user;
            webRoot = env.WebRootPath;

        }

        public async Task<TaskLogAttachment> DownloadAttachment(int attachId)
        {
            var attachment = await LogAttachmentRepository.GetTaskLogAttachmentById(attachId);

            if (attachment == null || attachment.IsDeleted)
                throw new FileNotFoundException("Attachment not found.");

            return attachment;
        }

        public async Task<TaskLogAttachment> DeleteAttachment(int attachId)
        {
            var attach = await LogAttachmentRepository.GetTaskLogAttachmentById(attachId);

            attach.IsDeleted = true;
            attach.LastModifiedAt = DateTime.Now;
            attach.LastModifiedBy = currentUser.Name;

            await LogAttachmentRepository.DeleteTaskLogAttachment();
            return attach;

        }
        public async Task DeleteAttachmentByLog(int Log)
        {
            var attach1 = await LogAttachmentRepository.GetAllTaskLogAttachmentByTaskLogId(Log);

            foreach (var attach in attach1)
            {
                attach.IsDeleted = true;
                attach.LastModifiedAt = DateTime.Now;
                attach.LastModifiedBy = currentUser.Name;
            }

            await LogAttachmentRepository.DeleteTaskLogAttachment();

        }

        public async Task<List<TaskLogAttachment>> GetAllAttachments(int logId)
        {
            var attach = await LogAttachmentRepository.GetAllTaskLogAttachmentByTaskLogId(logId);
            return attach;
        }

        public async Task<TaskLogAttachment> UploadAttachment(int logId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file.");

            var log = await TaskLogRepository.GetTaskLogByLogId(logId);

            if (log == null)
                throw new Exception("Task log not found.");

            string uploadsFolder = Path.Combine(webRoot, "Uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var attachment = new TaskLogAttachment
            {
                FileName = file.FileName,
                FilePath = $"Uploads/{uniqueFileName}",
                ContentType = file.ContentType,
                LogId = logId,
                CreatedAt = DateTime.Now,
                CreatedBy = currentUser.Name,
                LastModifiedAt = DateTime.Now,
                LastModifiedBy = currentUser.Name
            };

            await LogAttachmentRepository.CreateNewTaskLogAttachment(attachment);

            return attachment;
        }

    }
}
