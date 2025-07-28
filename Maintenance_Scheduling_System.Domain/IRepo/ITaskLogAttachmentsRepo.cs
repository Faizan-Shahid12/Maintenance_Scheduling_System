using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.IRepo
{
    public interface ITaskLogAttachmentsRepo
    {
        public Task CreateNewTaskLogAttachment(TaskLogAttachment tasklogs);
        public Task DeleteTaskLogAttachment(TaskLogAttachment taskLogs);
        public Task UpdateTaskLogAttachment(TaskLogAttachment tasklogs);
        public Task<List<TaskLogAttachment>> GetAllTaskLogAttachmentByTaskLogId(int LogId);
    }
}
