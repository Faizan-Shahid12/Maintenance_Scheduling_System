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
    public class ReadQRCodeByFileCommand : IRequest<EquipmentDTO>
    {
        public IFormFile _formFile;

        public ReadQRCodeByFileCommand(IFormFile formFile)
        {
            _formFile = formFile;
        }
    }
}
