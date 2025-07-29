using Maintenance_Scheduling_System.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Services
{
    public class FakeCurrentUser : ICurrentUser
    {
        public string? UserId => "123";
        public string? Name => "FaizanBack";
    }

}
