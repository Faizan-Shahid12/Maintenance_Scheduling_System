using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.IRepo
{
    public interface IMaintenanceScheduleRepo
    {
        public Task CreateNewMaintenanceSchedule(MaintenanceSchedule MainHis);
        public Task DeleteMaintenanceSchedule(MaintenanceSchedule MainHis);
        public Task UpdateMaintenanceSchedule(MaintenanceSchedule MainHis);
        public Task<List<MaintenanceSchedule>> GetMaintenanceScheduleByName(string Name);
        public Task<List<MaintenanceSchedule>> GetAllMaintenanceSchedule();
        public Task<List<MaintenanceSchedule>> GetAllMaintenanceScheduleByType(string Type);
        public Task<List<MaintenanceSchedule>> GetAllMaintenanceScheduleByStartDate(DateTime dates);
    }
}
