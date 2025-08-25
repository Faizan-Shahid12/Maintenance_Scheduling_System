using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Commands
{
    public class UpdateMaintenanceScheduleCommand : IRequest<DisplayMaintenanceScheduleDTO>
    {
        public MaintenanceScheduleDTO MaintenanceSchedule { get; set; }
        public UpdateMaintenanceScheduleCommand(MaintenanceScheduleDTO maintenanceSchedule)
        {
            MaintenanceSchedule = maintenanceSchedule;
        }
    }
}
