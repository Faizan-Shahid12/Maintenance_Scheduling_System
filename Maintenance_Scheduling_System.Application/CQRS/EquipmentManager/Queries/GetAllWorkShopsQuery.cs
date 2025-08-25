using Maintenance_Scheduling_System.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Queries
{
    public class GetAllWorkShopsQuery : IRequest<List<WorkShopDTO>>
    {
    }
}
