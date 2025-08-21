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
        Task<TaskLogDTO> CreateTaskLog(CreateTaskLogDTO taskdto);
        Task<List<TaskLogDTO>> GetAllTaskLog(int taskId);
        Task<TaskLogDTO> UpdateTaskLog(TaskLogDTO tasklog);
        Task<TaskLogDTO> DeleteTaskLog(int logId);
    }
}
