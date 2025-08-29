using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs
{
    public class TaskLogDTO
    {
        public int LogId { get; set; }

        public string Note { get; set; } = string.Empty;

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
