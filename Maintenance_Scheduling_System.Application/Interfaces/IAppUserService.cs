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
        Task CreateAppUser(AppUserDTO appuser, string role);
        Task DeleteAppUser(string id);
        Task UpdateAppUser(string Id, AppUserDTO appdto);
        Task<List<TechnicianDTO>> GetAllTechnicianUsers();
        Task<TokenResponseDTO> CreateToken(AppUser user);
        Task<AppUser> Login(LoginDTO LoginInfo);
    }
}
