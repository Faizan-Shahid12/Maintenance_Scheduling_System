using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Interfaces
{
    public interface IAppUserService
    {
        Task<TechnicianDTO> CreateAppUser(AppUserDTO appuser, string role);
        Task<TechnicianDTO> DeleteAppUser(string id);
        Task<TechnicianDTO> UpdateAppUser(string Id, AppUserDTO appdto);
        Task ChangePassword(string TechId, ChangePasswordDTO Password);
        Task<TechnicianDTO> GetTechnicianById(string TechId);
        Task<List<TechnicianDTO>> GetAllTechnicianUsersWithoutTask();
        Task<List<TechnicianDTO>> GetAllTechnicianUsers();
        Task<TokenResponseDTO> CreateToken(AppUser user);
        Task<AppUser> Login(LoginDTO LoginInfo);
        Task<bool> CheckEmail(string newEmail);

    }
}
