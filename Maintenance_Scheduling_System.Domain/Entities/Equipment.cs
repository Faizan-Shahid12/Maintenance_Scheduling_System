using Maintenance_Scheduling_System.Domain.Interface;

namespace Maintenance_Scheduling_System.Domain.Entities
{
    public class Equipment : IAudit
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string location { get; set; }
        public bool IsArchived { get; set; }



        public int? WorkshopId { get; set; }
        public WorkShopLoc? WorkShopLocation { get; set; }

        public List<MaintenanceSchedule> Schedule { get; set; }

        public List<MaintenanceHistory> History { get; set; }

        public List<MainTask> Tasks { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastModifiedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get;  set; } = string.Empty;
        public string LastModifiedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
