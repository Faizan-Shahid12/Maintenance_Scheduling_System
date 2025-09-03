using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.DTO.AttachmentDTOs
{
    public class FileDTO
    {
        public IFormFile File { get; set; }
    }
}
