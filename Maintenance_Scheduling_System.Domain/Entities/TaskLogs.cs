using Maintenance_Scheduling_System.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.Entities
{
    public class TaskLogs : IAudit
    {
        public int LogId { get; set; }
        public string Note { get; set; } = string.Empty;

        public DateTime LCreatedAt { get; set; } = DateTime.Now;
        public string LCreatedBy { get; set; } = string.Empty;

        public List<TaskLogAttachment>? Attachments { get; set; }

        public int TaskId { get; set; }
        public MainTask task {  get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastModifiedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public string LastModifiedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
