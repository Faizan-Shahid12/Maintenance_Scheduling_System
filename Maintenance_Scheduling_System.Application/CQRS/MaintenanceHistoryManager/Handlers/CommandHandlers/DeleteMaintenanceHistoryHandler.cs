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
    public class DeleteMaintenanceHistoryHandler : IRequestHandler<DeleteMaintenanceHistoryCommand>
    {
        private readonly IMaintenanceHistoryRepo _maintenanceHistoryRepository;

        public DeleteMaintenanceHistoryHandler(IMaintenanceHistoryRepo repo)
        {
            _maintenanceHistoryRepository = repo;
        }

        public async Task Handle(DeleteMaintenanceHistoryCommand request, CancellationToken cancellationToken)
        {
            var history = await _maintenanceHistoryRepository.GetMaintenanceHistory(request.HistoryId);
            if (history == null) throw new Exception("History not found");

            history.IsDeleted = true;

            await _maintenanceHistoryRepository.UpdateMaintenanceHistory();
        }

    }
}


