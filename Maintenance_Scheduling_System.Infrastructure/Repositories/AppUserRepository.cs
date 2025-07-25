using Maintenance_Scheduling_System.Domain.IRepo;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepo
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AppUserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
    }
}
