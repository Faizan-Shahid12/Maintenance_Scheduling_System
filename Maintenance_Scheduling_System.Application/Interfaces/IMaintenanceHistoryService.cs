using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Interfaces
{
    public interface IMaintenanceHistoryService
    {
        Task CreateMaintenanceHistoryFromTask(CreateMaintenanceHistoryDTO mainhisDTO, MainTask task,Equipment equip);
        Task AddNewTasktoHistory(int id, MainTask Task);
        Task EditHistory(EditMaintenanceHistoryDTO mainDto);
        Task DeleteHistory(int HistoryId);
        Task<List<DisplayMaintenanceHistoryDTO>> GetMaintenanceHistoryByEquipmentId(int EquipmentId);
        Task<List<EditMaintenanceHistoryDTO>> GetMaintenanceHistoryByEquipId(int EquipmentId);
        Task<List<DisplayMaintenanceHistoryDTO>> GetAllMaintenanceHistory();
    }
}
