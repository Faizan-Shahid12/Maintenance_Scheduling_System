using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Queries
{
    public class GetMaintenanceScheduleByIdQuery : IRequest<DisplayMaintenanceScheduleDTO?>
    {
        public int ScheduleId { get; set; }
        public GetMaintenanceScheduleByIdQuery(int scheduleId)
        {
            ScheduleId = scheduleId;
        }
    }
}
