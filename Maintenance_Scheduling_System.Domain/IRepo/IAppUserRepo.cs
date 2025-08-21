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
        public Task CreateNewAppUser(AppUser user, string password, string role);
        public Task DeleteAppUser(AppUser user);
        public Task UpdateAppUser(AppUser user);
        public Task<List<AppUser>> GetAllAppUser();
        public Task<AppUser> GetAppUserByEmail(string Name);
        public Task<List<AppUser>> GetAppUsersByRole(string Role);
        public Task<List<AppUser>> GetTechniciansUsers();
        public Task<List<AppUser>> GetTechniciansUsersWithoutTasks();
        public Task<AppUser> GetAppUserById(string id);
        public Task<List<string>> GetRoles(AppUser user);
        public Task ChangePassword(string TechId, string Password);
        public Task<bool> CheckEmail(string newEmail);



    }
}
