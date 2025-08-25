using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Commands
{
    public class UnArchiveEquipmentCommand : IRequest<EquipmentDTO>
    {
        public int EquipmentId { get; }

        public UnArchiveEquipmentCommand(int equipmentId)
        {
            EquipmentId = equipmentId;
        }
    }
}
