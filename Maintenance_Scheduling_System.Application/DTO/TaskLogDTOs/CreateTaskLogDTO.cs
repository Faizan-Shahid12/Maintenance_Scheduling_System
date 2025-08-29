using Maintenance_Scheduling_System.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs
{
    public class CreateTaskLogDTO
    {
        public int TaskId { get; set; }

        public string Note { get; set; } = string.Empty;

    }
}
