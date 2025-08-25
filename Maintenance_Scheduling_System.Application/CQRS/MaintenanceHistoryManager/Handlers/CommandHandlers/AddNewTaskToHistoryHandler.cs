using Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Commands;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Handlers.CommandHandlers
{
    public class AddNewTaskToHistoryHandler : IRequestHandler<AddNewTaskToHistoryCommand>
    {
        private readonly IMaintenanceHistoryRepo _maintenanceHistoryRepository;

        public AddNewTaskToHistoryHandler(IMaintenanceHistoryRepo maintenanceHistoryRepository)
        {
            _maintenanceHistoryRepository = maintenanceHistoryRepository;
        }

        public async Task Handle(AddNewTaskToHistoryCommand request, CancellationToken cancellationToken)
        {
            var main = await _maintenanceHistoryRepository.GetMaintenanceHistory(request.HistoryId);
            if (main == null) throw new Exception("History not found");

            main.AddTask(request.Task);
            RecalculateEndDate(main);

            await _maintenanceHistoryRepository.AddTask();
        }

        private void RecalculateEndDate(MaintenanceHistory history)
        {
            if (history.tasks != null && history.tasks.Any())
            {
                history.EndDate = history.tasks
                    .Where(t => t.DueDate != null)
                    .Max(t => t.DueDate);
            }
        }
    }
}
