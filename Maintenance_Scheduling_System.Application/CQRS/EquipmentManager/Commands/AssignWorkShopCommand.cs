using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Commands
{
    public class AssignWorkshopCommand : IRequest<EquipmentDTO>
    {
        public int EquipmentId { get; }
        public int WorkShopId { get; }

        public AssignWorkshopCommand(int equipmentId, int workShopId)
        {
            EquipmentId = equipmentId;
            WorkShopId = workShopId;
        }
    }
}
