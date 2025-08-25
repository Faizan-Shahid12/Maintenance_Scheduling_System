using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Commands
{
    public class CreateMaintenanceScheduleCommand : IRequest<DisplayMaintenanceScheduleDTO>
    {
        public CreateMaintenanceScheduleDTO MaintenanceSchedule { get; set; }
        public CreateMaintenanceScheduleCommand(CreateMaintenanceScheduleDTO maintenanceSchedule)
        {
            MaintenanceSchedule = maintenanceSchedule;
        }
    }
}
