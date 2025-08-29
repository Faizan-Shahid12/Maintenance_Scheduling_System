using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Interfaces
{
    public interface IMainTaskLogService
    {
        Task CreateTaskLog(CreateTaskLogDTO taskdto);
        Task<List<TaskLogDTO>> GetAllTaskLog(int taskId);
        Task UpdateTaskLog(TaskLogDTO tasklog);
        Task DeleteTaskLog(int logId);
    }
}
