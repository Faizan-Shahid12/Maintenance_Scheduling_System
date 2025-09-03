using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.BarCodeManager.Commands
{
    public class ReadQRCodeCommand : IRequest<EquipmentDTO>
    {
        public string BarCodeId;

        public ReadQRCodeCommand(string formFile)
        {
            BarCodeId = formFile;
        }
    }
}
