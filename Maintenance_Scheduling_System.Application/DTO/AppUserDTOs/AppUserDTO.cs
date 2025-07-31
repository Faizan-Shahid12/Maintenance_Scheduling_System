using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.DTO.AppUserDTOs
{
    public class AppUserDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Gender { get; set; }
    }
}
