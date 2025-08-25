using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Commands
{
    public class CreateEquipmentCommand : IRequest<EquipmentDTO>
    {
        public CreateEquipmentDTO EquipmentDTO { get; }

        public CreateEquipmentCommand(CreateEquipmentDTO equipmentDTO)
        {
            EquipmentDTO = equipmentDTO;
        }
    }
}
