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
    public class WorkShopLocRepository : IWorkShopLocRepo
    {
        private readonly Maintenance_DbContext DbContext;

        public WorkShopLocRepository(Maintenance_DbContext context)
        {
            DbContext = context;
        }

        public async Task<List<WorkShopLoc>> GetAllWorkShopLoc()
        {
             return await DbContext.WorkShopLocs.ToListAsync();
        }

        public async Task AddNewWorkShop(WorkShopLoc workShopLoc)
        {
            await DbContext.WorkShopLocs.AddAsync(workShopLoc);
            await DbContext.SaveChangesAsync();
            return;
        }

        public async Task<WorkShopLoc> GetWorkShopById(int workshopId)
        {
            return await DbContext.WorkShopLocs.FindAsync(workshopId);
        }

        public async Task<WorkShopLoc> GetWorkShopByLocation(string location)
        {
            return await DbContext.WorkShopLocs.FirstOrDefaultAsync(x => x.Location == location);
        }
    }
}
