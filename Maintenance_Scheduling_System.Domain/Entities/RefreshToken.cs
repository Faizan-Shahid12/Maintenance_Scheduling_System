using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.Entities
{
    public class RefreshToken
    {
        public int TokenId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public bool IsRevoked { get; set; } = false;
        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = null!;

    }
}
