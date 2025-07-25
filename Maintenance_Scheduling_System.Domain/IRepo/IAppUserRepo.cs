using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.IRepo
{
    public interface IAppUserRepo
    {
        public Task CreateNewAppUser(AppUser user);
        public Task DeleteAppUser(AppUser user);
        public Task UpdateAppUser(AppUser user);
        public Task<List<AppUser>> GetAppUserByName(string Name);
        public Task<List<AppUser>> GetAllAppUser();
        public Task<List<AppUser>> GetAppUsersByRole(string Role);
    }
}
