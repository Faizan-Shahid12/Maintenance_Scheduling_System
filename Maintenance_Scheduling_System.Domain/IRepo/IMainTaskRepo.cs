using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.IRepo
{
    public interface IMainTaskRepo
    {
        public Task CreateNewTask(MainTask Task1);
        public Task DeleteTask(MainTask Task1);
        public Task UpdateTask(MainTask Task1);
        public Task<MainTask> GetTask(string Name);
        public Task<List<MainTask>> GetAllTask();
        public Task<List<MainTask>> GetTaskByName(string Name);
        public Task<List<MainTask>> GetTaskByStatus(StatusEnum  Status);
    }
}
