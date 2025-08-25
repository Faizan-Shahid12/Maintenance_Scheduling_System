using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Commands;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Handler.CommandHandlers
{
    public class AutomaticallyUnactivateHandler : IRequestHandler<AutomaticallyUnactivateCommand>
    {
        private readonly IMaintenanceScheduleRepo _repo;
        private readonly IMapper _mapper;

        public AutomaticallyUnactivateHandler(IMaintenanceScheduleRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task Handle(AutomaticallyUnactivateCommand request, CancellationToken cancellationToken)
        {
            var schedules = await _repo.GetAllMaintenanceSchedule();

            foreach (var schedule in schedules)
            {
                if (schedule == null) continue;

                if (schedule.EndDate != null &&
                   (schedule.EndDate <= DateOnly.FromDateTime(DateTime.Now) || schedule.EndDate < schedule.StartDate))
                {
                    schedule.IsActive = false;
                    await _repo.UpdateMaintenanceSchedule();
                }
            }

            return ;
        }
    }
}
