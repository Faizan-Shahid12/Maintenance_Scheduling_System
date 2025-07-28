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
    }
}
