using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Maintenance_Scheduling_System.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.Repositories
{
    public class TaskLogAttachmentRepository : ITaskLogAttachmentsRepo
    {
        public Maintenance_DbContext DbContext { get; set; }

        public TaskLogAttachmentRepository(Maintenance_DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task CreateNewTaskLogAttachment(TaskLogAttachment tasklogs)
        {
            await DbContext.TaskLogsAttachments.AddAsync(tasklogs);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskLogAttachment(TaskLogAttachment taskLogs)
        {
            taskLogs.IsDeleted = true;
            taskLogs.LastModifiedAt = DateTime.UtcNow;

            DbContext.TaskLogsAttachments.Update(taskLogs);
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<TaskLogAttachment>> GetAllTaskLogAttachmentByTaskLogId(int logId)
        {
            return await DbContext.TaskLogsAttachments
                .Where(t => !t.IsDeleted && t.LogId == logId)
                .ToListAsync();
        }

        public async Task UpdateTaskLogAttachment(TaskLogAttachment tasklogs)
        {
            tasklogs.LastModifiedAt = DateTime.UtcNow;

            DbContext.TaskLogsAttachments.Update(tasklogs);
            await DbContext.SaveChangesAsync();
        }
    }
}
