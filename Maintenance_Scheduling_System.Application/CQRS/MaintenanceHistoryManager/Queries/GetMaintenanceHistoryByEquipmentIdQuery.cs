using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Queries
{
    public class GetMaintenanceHistoryByEquipmentIdQuery : IRequest<List<MaintenanceHistoryDTO>>
    {
        public int EquipmentId { get; set; }

        public GetMaintenanceHistoryByEquipmentIdQuery(int equipmentId)
        {
            EquipmentId = equipmentId;
        }
    }
}
