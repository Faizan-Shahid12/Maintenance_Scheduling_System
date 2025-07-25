using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.IRepo
{
    public interface IMaintenanceHistoryRepo
    {
        public Task CreateNewMaintenanceHistory(MaintenanceHistory MainHis);
        public Task DeleteMaintenanceHistory(MaintenanceHistory MainHis);
        public Task UpdateMaintenanceHistory(MaintenanceHistory MainHis);
        public Task<MaintenanceHistory> GetMaintenanceHistory(string EquipName);
        public Task<List<MaintenanceHistory>> GetAllMaintenanceHistory();
    }
}
