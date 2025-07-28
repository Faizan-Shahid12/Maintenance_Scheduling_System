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
    public class TaskLogsRepository : ITaskLogRepo
    {
        private readonly Maintenance_DbContext DbContext;

        public TaskLogsRepository(Maintenance_DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task CreateNewTaskLogs(TaskLogs tasklogs)
        {
            await DbContext.TaskLogs.AddAsync(tasklogs);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskLogs(TaskLogs taskLogs)
        {
            taskLogs.IsDeleted = true; 
            taskLogs.LastModifiedAt = DateTime.UtcNow;
            DbContext.TaskLogs.Update(taskLogs);
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<TaskLogs>> GetAllTaskLogsByTaskId(int taskId)
        {
            return await DbContext.TaskLogs
                .Where(t => t.TaskId == taskId && !t.IsDeleted)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateTaskLogs(TaskLogs tasklogs)
        {
            tasklogs.LastModifiedAt = DateTime.UtcNow;
            DbContext.TaskLogs.Update(tasklogs);
            await DbContext.SaveChangesAsync();
        }
    }
}
