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
        Task CreateMaintenanceSchedule(CreateMaintenanceScheduleDTO msdto);
        Task UpdateMaintenanceSchedule(MaintenanceScheduleDTO msdto);
        Task DeleteMaintenanceSchedule(int MSId);
        Task AddNewTasktoSchedule(int ScheduleId, CreateScheduleTaskDTO stdto);
        Task DeleteTaskFromSchedule(int ScheduleId, int ScheduleTaskId);
        Task UnActivateSchedule(int ScheduleId);
        Task ActivateSchedule(int ScheduleId);
        Task<List<DisplayMaintenanceScheduleDTO>> GetMaintenanceScheduleByEquipmentId(int EquipmentId);
        Task<List<DisplayMaintenanceScheduleDTO>> GetAllMaintenanceSchedule();
        Task<List<DisplayMaintenanceScheduleDTO>> GetAllMaintenanceScheduleByStartDate();
        Task AutomaticallyUnactivate();
        Task AutomaticallyGenerate();
    }
}
