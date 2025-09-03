using Maintenance_Scheduling_System.Application.DTO.BarCodeDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.BarCodeManager.Queries
{
    public class GenerateQRCodeQuery : IRequest<List<BarCodeResultDTO>>
    {
        public List<int> EquipmentId { get; set; }

        public GenerateQRCodeQuery(List<int> EquipId)
        {
            EquipmentId = EquipId;
        }
    }
}
