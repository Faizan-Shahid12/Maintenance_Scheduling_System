using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Interfaces
{
    public interface IMainTaskService
    {
        Task CreateNewMainTask(int EquipId, CreateMainTaskDTO Maintask);
        Task CreateNewMainTaskByScheduleTask(int EquipId, List<ScheduleTask> scheduleTasks);
        Task<List<MainTaskDTO>> GetAllMainTask();
        Task<List<MainTaskDTO>> GetMainTaskByEquipmentId(int EquipId);
        Task DeleteTask(int TaskId);
        Task UpdateTask(MainTaskDTO taskdto);
        Task UpdatePriority(int TaskId, PriorityEnum priority);
        Task ChangeTaskStatus(int TaskId, StatusEnum status);
        Task AssignTechnician(int TaskId, string TechId);
        Task<List<MainTaskDTO>> GetAllOverDueTask();
        Task<List<MainTaskDTO>> GetAllCompletedTask();
        Task OverDueTask();

    }
}
