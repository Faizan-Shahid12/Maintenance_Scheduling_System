using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Queries
{
    public class GetMainTasksByEquipmentIdQuery : IRequest<List<MainTaskDTO>>
    {
        public int EquipId { get; set; }
        public GetMainTasksByEquipmentIdQuery(int equipId) => EquipId = equipId;
    }
}
