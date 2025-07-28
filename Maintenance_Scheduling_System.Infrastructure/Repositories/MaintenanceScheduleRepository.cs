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

        public async Task DeleteMaintenanceSchedule(MaintenanceSchedule MainHis)
        {
            MainHis.IsDeleted = true;
            MainHis.LastModifiedAt = DateTime.UtcNow;
            DbContext.MaintenanceSchedules.Update(MainHis);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateMaintenanceSchedule(MaintenanceSchedule MainHis)
        {
            var existing = await DbContext.MaintenanceSchedules
                .Where(x => x.ScheduleId == MainHis.ScheduleId && !x.IsDeleted)
                .FirstOrDefaultAsync();

            if (existing == null)
                throw new Exception("Schedule not found");

            existing.ScheduleType = MainHis.ScheduleType;
            existing.StartDate = MainHis.StartDate;
            existing.EndDate = MainHis.EndDate;
            existing.ScheduleTasks = MainHis.ScheduleTasks;
            existing.LastModifiedAt = DateTime.UtcNow;
            existing.LastModifiedBy = MainHis.LastModifiedBy;

            await DbContext.SaveChangesAsync();
        }

        public async Task<List<MaintenanceSchedule>> GetAllMaintenanceSchedule()
        {
            return await DbContext.MaintenanceSchedules
                .Where(ms => !ms.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<MaintenanceSchedule>> GetAllMaintenanceScheduleByStartDate(DateOnly date)
        {
            return await DbContext.MaintenanceSchedules
                .Where(ms => !ms.IsDeleted && ms.StartDate == date)
                .ToListAsync();
        }

        public async Task<List<MaintenanceSchedule>> GetAllMaintenanceScheduleByType(string type)
        {
            return await DbContext.MaintenanceSchedules
                .Where(ms => !ms.IsDeleted && ms.ScheduleType.ToLower() == type.ToLower())
                .ToListAsync();
        }

        public async Task<List<MaintenanceSchedule>> GetMaintenanceScheduleByName(string name)
        {
            return await DbContext.MaintenanceSchedules
                .Where(ms => !ms.IsDeleted && ms.ScheduleName.ToLower() == name.ToLower())
                .ToListAsync();
        }
    }
}
