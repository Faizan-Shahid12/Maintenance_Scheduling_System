using Maintenance_Scheduling_System.Domain.Entities;
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

        public Task CreateNewAppUser(AppUser user, string password)
        {
            _userManager.CreateAsync(user,password);
        }

        public Task DeleteAppUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppUser>> GetAllAppUser()
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetAppUserByUserName(string Name)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppUser>> GetAppUsersByRole(string Role)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAppUser(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
