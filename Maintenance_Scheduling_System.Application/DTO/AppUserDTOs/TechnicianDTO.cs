using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.DTO.AppUserDTOs
{
    public class TechnicianDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Gender { get; set; }
        public List<MainTask>? AssignedTasks { get; set; }

    }

}
