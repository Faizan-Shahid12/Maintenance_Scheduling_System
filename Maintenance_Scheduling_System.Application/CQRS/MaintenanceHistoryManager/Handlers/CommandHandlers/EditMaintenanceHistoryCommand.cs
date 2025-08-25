using AutoMapper;
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
    public class EditMaintenanceHistoryHandler : IRequestHandler<EditMaintenanceHistoryCommand>
    {
        private readonly IMaintenanceHistoryRepo _maintenanceHistoryRepository;
        private readonly IMapper _mapper;

        public EditMaintenanceHistoryHandler(IMaintenanceHistoryRepo repo, IMapper mapper)
        {
            _maintenanceHistoryRepository = repo;
            _mapper = mapper;
        }

        public async Task Handle(EditMaintenanceHistoryCommand request, CancellationToken cancellationToken)
        {
            var dto = request.MaintenanceHistoryDTO;
            var existing = await _maintenanceHistoryRepository.GetMaintenanceHistory(dto.HistoryId);

            if (existing == null) throw new Exception("History does not exist");

            existing.EquipmentName = dto.EquipmentName;
            existing.EquipmentType = dto.EquipmentType;
            existing.StartDate = dto.StartDate;
            existing.EndDate = dto.EndDate;

            RecalculateEndDate(existing);

            await _maintenanceHistoryRepository.UpdateMaintenanceHistory();
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
