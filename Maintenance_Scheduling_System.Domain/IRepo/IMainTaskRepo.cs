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
        public Task<MainTask> CreateNewTask(MainTask Task1);
        public Task DeleteTask();
        public Task UpdateTask();
        public Task<MainTask> GetTaskById(int Id);
        public Task<List<MainTask>> GetAllTaskByEquipId(int Id);
        public Task<List<MainTask>> GetAllTask();
        public Task<List<MainTask>> GetTaskByName(string Name);
        public Task<List<MainTask>> GetTaskByStatus(StatusEnum  Status);
        public Task<List<MainTask>> GetMainTaskByHistoryId(int HistoryId);
        public Task UnAssignTechnicianTask(string TechId);
        public Task<int> TotalCountofTask();


    }
}
