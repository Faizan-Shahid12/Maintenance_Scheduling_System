using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Commands
{
    public class DeleteMaintenanceHistoryCommand : IRequest
    {
        public int HistoryId { get; set; }

        public DeleteMaintenanceHistoryCommand(int historyId)
        {
            HistoryId = historyId;
        }
    }
}
