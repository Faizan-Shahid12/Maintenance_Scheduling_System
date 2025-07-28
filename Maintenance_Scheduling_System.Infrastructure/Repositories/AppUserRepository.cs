using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Maintenance_Scheduling_System.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly Maintenance_DbContext DbContext;


        public AppUserRepository(UserManager<AppUser> userManager, Maintenance_DbContext context)
        {
            _userManager = userManager;
            DbContext = context;
        }

        public async Task CreateNewAppUser(AppUser user, string password,string role)
        {
            await _userManager.CreateAsync(user,password);
            await _userManager.AddToRoleAsync(user, role);
            await DbContext.SaveChangesAsync();
           
        }

        public async Task DeleteAppUser(AppUser user)
        {
            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);
        }

        public async Task<List<AppUser>> GetAllAppUser()
        {
            return await DbContext.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetAppUserByUserName(string Name)
        {
            var user = await _userManager.FindByNameAsync(Name);

            if (user == null) return null;

            return user;
        }

        public async Task<List<AppUser>> GetAppUsersByRole(string Role)
        {
            var users = await _userManager.GetUsersInRoleAsync(Role);
            return users.ToList();
        }

        public async Task UpdateAppUser(AppUser user)
        {
           DbContext.AppUsers.Update(user);
           await DbContext.SaveChangesAsync();
           
        }
    }
}
