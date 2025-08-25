using Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Queries;
using Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Handler.CommandHandlers
{
    public class AutomaticallyGenerateHandler : IRequestHandler<AutomaticallyGenerateCommand>
    {
        private readonly IMaintenanceScheduleRepo _repo;
        private readonly IMediator _mediator;

        public AutomaticallyGenerateHandler(IMaintenanceScheduleRepo repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }

        public async Task Handle(AutomaticallyGenerateCommand request, CancellationToken cancellationToken)
        {
            var schedules = await _repo.GetAllMaintenanceSchedule();
            var equipments = await _mediator.Send(new GetAllEquipmentsQuery());

            foreach (var schedule in schedules)
            {
                if (schedule == null) continue;

                var equipment = equipments.FirstOrDefault(e => e.EquipmentId == schedule.EquipmentId);

                if (equipment == null || equipment.isArchived) continue;

                if (schedule.StartDate <= DateOnly.FromDateTime(DateTime.Now) && schedule.IsActive)
                {
                    schedule.LastGeneratedDate = schedule.StartDate;
                    schedule.StartDate = schedule.StartDate.AddDays((int)schedule.Interval.TotalDays);

                    await _mediator.Send(new CreateNewMainTaskByScheduleTaskCommand(schedule.EquipmentId, schedule.ScheduleTasks));
                    await _mediator.Send(new ChangeDueDateCommand(schedule.StartDate, schedule.ScheduleTasks));

                }
            }

            return ;
        }
    }
}
