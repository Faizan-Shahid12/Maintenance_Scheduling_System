using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Maintenance_Scheduling_System.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.Repositories
{
    public class MaintenanceHistoryRepository : IMaintenanceHistoryRepo
    {
        public Maintenance_DbContext DbContext;

        public MaintenanceHistoryRepository(Maintenance_DbContext context)
        {
            DbContext = context;
        }

        public async Task CreateNewMaintenanceHistory(MaintenanceHistory mainHis)
        {
            await DbContext.MaintenanceHistories.AddAsync(mainHis);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteMaintenanceHistory(MaintenanceHistory mainHis)
        {
            var existing = await DbContext.MaintenanceHistories.FindAsync(mainHis.HistoryId);

            if (existing != null)
            {
                existing.IsDeleted = true;
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<List<MaintenanceHistory>> GetAllMaintenanceHistory()
        {
            return await DbContext.MaintenanceHistories.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<MaintenanceHistory>> GetMaintenanceHistoryByEquipId(int equipId)
        {
            return await DbContext.MaintenanceHistories.Where(x => x.IsDeleted == false && x.EquipmentId == equipId).ToListAsync();
        }

        public async Task<MaintenanceHistory?> GetMaintenanceHistory(int Id)
        {
            return await DbContext.MaintenanceHistories.Where( x => x.HistoryId == Id && x.IsDeleted == false).FirstOrDefaultAsync();
        }

        public async Task AddTask()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateMaintenanceHistory()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task<int> TotalCountOfMaintenance()
        {
            var num = await DbContext.MaintenanceHistories.CountAsync();
            return num;
        }
    }
}
