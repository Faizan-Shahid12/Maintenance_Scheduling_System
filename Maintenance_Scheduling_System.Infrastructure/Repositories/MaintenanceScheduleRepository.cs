using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Maintenance_Scheduling_System.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.Repositories
{
    public class MaintenanceScheduleRepository : IMaintenanceScheduleRepo
    {
        public Maintenance_DbContext DbContext { get; set; }

        public MaintenanceScheduleRepository(Maintenance_DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task CreateNewMaintenanceSchedule(MaintenanceSchedule MainHis)
        {
            await DbContext.MaintenanceSchedules.AddAsync(MainHis);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteMaintenanceSchedule()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateMaintenanceSchedule()
        {
             await DbContext.SaveChangesAsync();
        }

        public async Task<List<MaintenanceSchedule>> GetAllMaintenanceSchedule()
        {
            return await DbContext.MaintenanceSchedules.Include(ms => ms.ScheduleTasks.Where(st => !st.IsDeleted))
                .Where(ms => !ms.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<MaintenanceSchedule>> GetAllMaintenanceScheduleByStartDate(DateOnly date)
        {
            return await DbContext.MaintenanceSchedules.Include(ms => ms.ScheduleTasks.Where(st => !st.IsDeleted))
                .Where(ms => !ms.IsDeleted && ms.StartDate == date)
                .ToListAsync();
        }

        public async Task<List<MaintenanceSchedule>> GetAllMaintenanceScheduleByType(string type)
        {
            return await DbContext.MaintenanceSchedules.Include(ms => ms.ScheduleTasks.Where(st => !st.IsDeleted))
                .Where(ms => !ms.IsDeleted && ms.ScheduleType.ToLower() == type.ToLower())
                .ToListAsync();
        }

        public async Task<List<MaintenanceSchedule>> GetMaintenanceScheduleByName(string name)
        {
            return await DbContext.MaintenanceSchedules.Include(ms => ms.ScheduleTasks.Where(st => !st.IsDeleted))
                .Where(ms => !ms.IsDeleted && ms.ScheduleName.ToLower() == name.ToLower())
                .ToListAsync();
        }
        public async Task<MaintenanceSchedule> GetMaintenanceScheduleById(int id)
        {
            return await DbContext.MaintenanceSchedules.Include(ms => ms.ScheduleTasks.Where(st => !st.IsDeleted))
                .Where(ms => !ms.IsDeleted && ms.ScheduleId == id).FirstOrDefaultAsync();
        }
        public async Task<List<MaintenanceSchedule>> GetAllMaintenanceScheduleByEquipId(int EquipId)
        {
            return await DbContext.MaintenanceSchedules.Include(ms => ms.ScheduleTasks.Where(st => !st.IsDeleted))
                .Where(ms => !ms.IsDeleted && ms.EquipmentId == EquipId)
                .ToListAsync();
        }
    }
}
