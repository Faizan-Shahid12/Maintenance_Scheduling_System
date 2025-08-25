using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Queries
{
    public class GetMaintenanceSchedulesByEquipmentIdQuery : IRequest<List<DisplayMaintenanceScheduleDTO>>
    {
        public int EquipmentId { get; set; }
        public GetMaintenanceSchedulesByEquipmentIdQuery(int equipmentId)
        {
            EquipmentId = equipmentId;
        }
    }
}
