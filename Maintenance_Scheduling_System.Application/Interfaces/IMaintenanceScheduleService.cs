using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Interfaces
{
    public interface IMaintenanceScheduleService
    {
        Task<DisplayMaintenanceScheduleDTO> CreateMaintenanceSchedule(CreateMaintenanceScheduleDTO msdto);
        Task<DisplayMaintenanceScheduleDTO> UpdateMaintenanceSchedule(MaintenanceScheduleDTO msdto);
        Task<DisplayMaintenanceScheduleDTO> DeleteMaintenanceSchedule(int MSId);
        Task<DisplayMaintenanceScheduleDTO> AddNewTasktoSchedule(int ScheduleId, CreateScheduleTaskDTO stdto);
        Task<DisplayMaintenanceScheduleDTO> DeleteTaskFromSchedule(int ScheduleId, int ScheduleTaskId);
        Task<DisplayMaintenanceScheduleDTO> UnActivateSchedule(int ScheduleId);
        Task<DisplayMaintenanceScheduleDTO> ActivateSchedule(int ScheduleId);
        Task<DisplayMaintenanceScheduleDTO> EditScheduleTask(int ScheduleId, ScheduleTaskDTO scheduleTaskDTO);
        Task<DisplayMaintenanceScheduleDTO> AssignTechnicianToScheduleTask(int ScheduleId, int ScheduleTaskId, string? TechId);
        Task<List<DisplayMaintenanceScheduleDTO>> GetMaintenanceScheduleByEquipmentId(int EquipmentId);
        Task<List<DisplayMaintenanceScheduleDTO>> GetAllMaintenanceSchedule();
        Task<List<DisplayMaintenanceScheduleDTO>> GetAllMaintenanceScheduleByStartDate();
        Task AutomaticallyUnactivate();
        Task AutomaticallyGenerate();
    }
}
