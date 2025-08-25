using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Queries
{
    public class GetAllEquipmentsQuery : IRequest<List<EquipmentDTO>>
    {
    }
}
