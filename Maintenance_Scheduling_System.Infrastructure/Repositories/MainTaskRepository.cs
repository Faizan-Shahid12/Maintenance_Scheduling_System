using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.Enums;
using Maintenance_Scheduling_System.Domain.IRepo;
using Maintenance_Scheduling_System.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.Repositories
{
    public class MainTaskRepository : IMainTaskRepo
    {
        public Maintenance_DbContext DbContext { get; set; }

        public MainTaskRepository(Maintenance_DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<MainTask> CreateNewTask(MainTask task)
        {
            await DbContext.MainTask.AddAsync(task);
            await DbContext.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTask()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateTask()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task<MainTask?> GetTaskById(int id)
        {
            return await DbContext.MainTask
                .Where(t => t.TaskId == id && !t.IsDeleted)
                .Include(t => t.Technician)
                .FirstOrDefaultAsync();
        }

        public async Task<List<MainTask>> GetAllTask()
        {
            return await DbContext.MainTask
                .Where(t => !t.IsDeleted)
                .Include(t => t.Technician)
                .ToListAsync();
        }
        public async Task<List<MainTask>> GetAllTaskByEquipId(int Id)
        {
            return await DbContext.MainTask
                .Where(t => !t.IsDeleted && t.EquipmentId == Id)
                .Include (t => t.Technician)
                .ToListAsync();
        }

        public async Task<List<MainTask>> GetTaskByName(string name)
        {
            return await DbContext.MainTask
                .Where(t => t.TaskName.ToLower() == name.ToLower() && !t.IsDeleted)
                .Include(t => t.Technician)
                .ToListAsync();
        }

        public async Task<List<MainTask>> GetTaskByStatus(StatusEnum status)
        {
            return await DbContext.MainTask
                .Where(t => t.Status == status && !t.IsDeleted)
                .Include(t => t.Technician)
                .ToListAsync();
        }

        public async Task<List<MainTask>> GetMainTaskByHistoryId(int HistoryId)
        {
            return await DbContext.MainTask.Where(t => t.HistoryId == HistoryId && !t.IsDeleted).Include(t => t.Technician).ToListAsync();
        }

        public async Task UnAssignTechnicianTask(string TechId)
        {
            var tasks = await DbContext.MainTask
                .Where(t => t.TechnicianId == TechId)
                .ToListAsync();

            foreach (var task in tasks)
            {
                task.TechnicianId = null;
                task.Technician = null;
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task<int> TotalCountofTask()
        {
            return await DbContext.MainTask.CountAsync();
        }

        public async Task LoadTechnician(MainTask task)
        {
            await DbContext.Entry(task).Reference(t => t.Technician).LoadAsync();
        }
    }
}
