using Maintenance_Scheduling_System.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Services
{
    public class FakeUserService : ICurrentUser
    {
        public string? UserId => "AS";

        public string? Name => "Faizan12345689r";

        public string? Role => "Admin";
    }
}
