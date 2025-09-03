using Maintenance_Scheduling_System.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Interfaces
{
    public interface IBarCodeService
    {
        public byte[] GenerateQRCode(Guid EquipId);
        public string ReadQRCodeByFile(IFormFile file);
        public string ReadQRCode(string Decodedbarcode);

    }
}
