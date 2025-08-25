using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Commands
{
    public class DeleteEquipmentCommand : IRequest<EquipmentDTO>
    {
        public int EquipId {  get; set; }

        public DeleteEquipmentCommand(int equipId)
        {
            EquipId = equipId;
        }
    }
}
