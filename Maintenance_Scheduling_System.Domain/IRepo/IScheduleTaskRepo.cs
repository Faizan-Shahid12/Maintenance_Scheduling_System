using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.IRepo
{
    public interface IScheduleTaskRepo
    {
        public Task CreateNewScheduleTask(ScheduleTask equip);
        public Task DeleteScheduleTask();
        public Task UpdateScheduleTask(ScheduleTask task);
        public Task<ScheduleTask> GetScheduleTask(int ScheduleId);
        public Task<List<ScheduleTask>> GetAllScheduleTask(int id);
    }
}
