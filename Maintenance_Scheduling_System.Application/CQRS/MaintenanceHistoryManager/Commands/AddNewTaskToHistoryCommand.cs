using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Commands
{
    public class AddNewTaskToHistoryCommand : IRequest
    {
        public int HistoryId { get; set; }
        public MainTask Task { get; set; }

        public AddNewTaskToHistoryCommand(int historyId, MainTask task)
        {
            HistoryId = historyId;
            Task = task;
        }
    }
}
