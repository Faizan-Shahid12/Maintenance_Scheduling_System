using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Commands
{
    public class ActivateScheduleCommand : IRequest<DisplayMaintenanceScheduleDTO>
    {
        public int ScheduleId { get; set; }
        public ActivateScheduleCommand(int scheduleId)
        {
            ScheduleId = scheduleId;
        }
    }

}
