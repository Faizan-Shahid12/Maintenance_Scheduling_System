using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.DTO.AppUserDTOs
{
    public class TokenResponseDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiry { get; set; }
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
