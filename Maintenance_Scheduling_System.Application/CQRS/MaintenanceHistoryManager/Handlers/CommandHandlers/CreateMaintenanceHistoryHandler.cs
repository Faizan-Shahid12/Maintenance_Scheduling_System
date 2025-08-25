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
    public class CreateMaintenanceHistoryFromTaskHandler : IRequestHandler<CreateMaintenanceHistoryFromTaskCommand>
    {
        private readonly IMaintenanceHistoryRepo _maintenanceHistoryRepository;
        private readonly IMapper _mapper;

        public CreateMaintenanceHistoryFromTaskHandler(
            IMaintenanceHistoryRepo maintenanceHistoryRepository,
            IMapper mapper)
        {
            _maintenanceHistoryRepository = maintenanceHistoryRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateMaintenanceHistoryFromTaskCommand request, CancellationToken cancellationToken)
        {
            var mainhis = _mapper.Map<MaintenanceHistory>(request.MainHistoryDTO);

            mainhis.AddTask(request.Task);

            RecalculateEndDate(mainhis);

            request.Equip.AddMaintenance(mainhis);

            await _maintenanceHistoryRepository.CreateNewMaintenanceHistory(mainhis);

            return;
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
