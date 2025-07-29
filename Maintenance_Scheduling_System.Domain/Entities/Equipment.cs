using Maintenance_Scheduling_System.Domain.Interface;

namespace Maintenance_Scheduling_System.Domain.Entities
{
    public class Equipment : IAudit
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string location { get; set; }
        public string SerialNumber { get;  set; }
        public string Model { get;  set; }
        public bool IsArchived { get; set; } = false;


        public int? WorkshopId { get; set; } = null;
        public WorkShopLoc? WorkShopLocation { get; set; } = null;
        public List<MaintenanceSchedule> Schedule { get; set; } = new();

        public List<MaintenanceHistory> History { get; set; } = new();

        public List<MainTask> Tasks { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastModifiedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get;  set; } = string.Empty;
        public string LastModifiedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

        public int AssignWorkShopLocation(int workshopid)
        {
            if( IsArchived == true)
            {
                return -1;
            }
            WorkshopId = workshopid;

            return 1;
        }
        public void Archive()
        {
            IsArchived = true;
        }
        public void UnArchive()
        {
            IsArchived = false;
        }
        public int AssignType(string type)
        {
            if (IsArchived == true)
            {
                return -1;
            }
            Type = type;

            return 1;
        }
        public void AddMaintenance(MaintenanceHistory history)
        {
         if(IsArchived == false)
            { 
                if( History == null )
                    History = new();
             
                History.Add(history);
            }
        }
        public void AddMainTask(MainTask task)
        {
            if (IsArchived == false)
            {
                if (Tasks == null)
                    Tasks = new List<MainTask>();

                Tasks.Add(task);
            }
        }
    }
}
