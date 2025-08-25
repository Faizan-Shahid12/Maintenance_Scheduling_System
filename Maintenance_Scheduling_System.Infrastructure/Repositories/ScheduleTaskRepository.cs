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
    public class ScheduleTaskRepository : IScheduleTaskRepo
    {
        public Maintenance_DbContext DbContext { get; set; }

        public ScheduleTaskRepository(Maintenance_DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task CreateNewScheduleTask(ScheduleTask task)
        {
            await DbContext.ScheduleTasks.AddAsync(task);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteScheduleTask()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<ScheduleTask>> GetAllScheduleTask(int id)
        {
            return await DbContext.ScheduleTasks
                .Where(t => t.ScheduleTaskId == id && !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<ScheduleTask?> GetScheduleTask(int scheduleTaskId)
        {
            return await DbContext.ScheduleTasks
                .Where(t => t.ScheduleTaskId == scheduleTaskId && !t.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateScheduleTask(ScheduleTask task)
        {
            DbContext.ScheduleTasks.Update(task);
            await DbContext.SaveChangesAsync();
        }
    }
}
