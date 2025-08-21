using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.IRepo
{
    public interface ITaskLogRepo
    {
        public Task CreateNewTaskLogs(TaskLogs tasklogs);
        public Task DeleteTaskLogs(TaskLogs taskLogs);
        public Task UpdateTaskLogs(TaskLogs tasklogs);
        public Task<List<TaskLogs>> GetAllTaskLogsByTaskId(int TaskId);
        public Task<TaskLogs> GetTaskLogByLogId(int logId);
        public Task<int> TotalCountofTaskLog();

    }
}
