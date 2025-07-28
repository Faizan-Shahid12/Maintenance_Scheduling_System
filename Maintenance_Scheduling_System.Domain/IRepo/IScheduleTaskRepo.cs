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
        public Task DeleteScheduleTask(ScheduleTask equip);
        public Task UpdateScheduleTask(ScheduleTask equip);
        public Task<ScheduleTask> GetScheduleTask(int ScheduleId);
        public Task<List<ScheduleTask>> GetAllScheduleTask(int id);
    }
}
