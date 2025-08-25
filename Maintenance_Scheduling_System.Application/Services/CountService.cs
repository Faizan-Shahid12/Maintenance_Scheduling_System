using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Services
{
    //DONE
    public class CountService : ICountService
    {
        private IMaintenanceHistoryRepo _historyRepo;
        private IMainTaskRepo _mainTaskRepo;
        private ITaskLogRepo _taskLogRepo;
        private ITaskLogAttachmentsRepo _taskLogAttachmentsRepo;

        public CountService(IMaintenanceHistoryRepo historyRepo, IMainTaskRepo mainTaskRepo, ITaskLogRepo taskLogRepo, ITaskLogAttachmentsRepo taskLogAttachmentsRepo)
        {
            _historyRepo = historyRepo;
            _mainTaskRepo = mainTaskRepo;
            _taskLogRepo = taskLogRepo;
            _taskLogAttachmentsRepo = taskLogAttachmentsRepo;
        }

        public async Task<List<int>> GetTotalCount()
        {
            List<int> counts = new List<int>();

            var main = await _historyRepo.TotalCountOfMaintenance();
            var task = await _mainTaskRepo.TotalCountofTask();
            var taskLog = await _taskLogRepo.TotalCountofTaskLog();
            var taskLogAttach = await _taskLogAttachmentsRepo.TotalCountofLogAttachment();


            counts.Add(main);
            counts.Add(task);
            counts.Add(taskLog);
            counts.Add(taskLogAttach);

            return counts;

        }
    }
}
