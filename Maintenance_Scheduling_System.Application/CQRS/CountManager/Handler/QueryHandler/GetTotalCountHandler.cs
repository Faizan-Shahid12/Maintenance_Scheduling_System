using Maintenance_Scheduling_System.Application.CQRS.CountManager.Queries;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.CountManager.Handler.QueryHandler
{
    public class GetTotalCountHandler : IRequestHandler<GetTotalCountQuery, List<int>>
    {
        private readonly IMaintenanceHistoryRepo _historyRepo;
        private readonly IMainTaskRepo _mainTaskRepo;
        private readonly ITaskLogRepo _taskLogRepo;
        private readonly ITaskLogAttachmentsRepo _taskLogAttachmentsRepo;

        public GetTotalCountHandler(IMaintenanceHistoryRepo historyRepo,IMainTaskRepo mainTaskRepo,ITaskLogRepo taskLogRepo,ITaskLogAttachmentsRepo taskLogAttachmentsRepo)
        {
            _historyRepo = historyRepo;
            _mainTaskRepo = mainTaskRepo;
            _taskLogRepo = taskLogRepo;
            _taskLogAttachmentsRepo = taskLogAttachmentsRepo;
        }

        public async Task<List<int>> Handle(GetTotalCountQuery request, CancellationToken cancellationToken)
        {
            var counts = new List<int>
            {
                await _historyRepo.TotalCountOfMaintenance(),
                await _mainTaskRepo.TotalCountofTask(),
                await _taskLogRepo.TotalCountofTaskLog(),
                await _taskLogAttachmentsRepo.TotalCountofLogAttachment()
            };

            return counts;
        }
    }
}
