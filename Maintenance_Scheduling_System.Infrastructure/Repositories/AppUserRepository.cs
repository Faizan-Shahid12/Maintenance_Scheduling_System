using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
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
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errors}");
            }

            await _userManager.AddToRoleAsync(user, role);
                       
        }

        public async Task DeleteAppUser(AppUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<List<AppUser>> GetAllAppUser()
        {
            return await DbContext.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetAppUserByEmail(string Name)
        {
            var user = await _userManager.FindByEmailAsync(Name);

            if (user == null) return null;

            return user;
        }

        public async Task<List<AppUser>> GetAppUsersByRole(string Role)
        {
            var users = await _userManager.GetUsersInRoleAsync(Role);
            return users.ToList();
        }
        public async  Task<List<AppUser>> GetTechniciansUsers()
        {
            var users = (await _userManager.GetUsersInRoleAsync("Technician")).Where(u => !u.IsDeleted).ToList();

            foreach (AppUser user in users)
            {
                var tasks = await DbContext.MainTask.Where(u => !u.IsDeleted && u.TechnicianId == user.Id).ToListAsync();
                user.AssignedTasks = tasks;
            }
           
            return users;
        }

        public async Task<List<string>> GetRoles(AppUser user)
        {
            var dbUser = await _userManager.FindByIdAsync(user.Id);

            if (dbUser == null)
                throw new Exception("User not found");

            return (await _userManager.GetRolesAsync(dbUser)).ToList();
        }

        public async Task<AppUser> GetAppUserById(string id)
        {
            var user  = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task UpdateAppUser(AppUser user)
        {
            await _userManager.UpdateAsync(user);
        }
    }
}
