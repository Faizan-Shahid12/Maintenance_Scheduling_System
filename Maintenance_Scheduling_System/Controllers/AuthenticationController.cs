using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IRefreshTokenService RefreshTokenService;

        public AuthenticationController(IAppUserService appUserService, IRefreshTokenService rtser)
        {
            _appUserService = appUserService;
            RefreshTokenService = rtser;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTechnician([FromBody] AppUserDTO request)
        {
           var tech =  await _appUserService.CreateAppUser(request, "Technician");
            return Ok(tech);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAdmin([FromQuery] AppUserDTO request)
        {
            await _appUserService.CreateAppUser(request, "Admin");
            return Ok("Admin created successfully.");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTechnician([FromQuery] string id)
        {
            var tech = await _appUserService.DeleteAppUser(id);
            return Ok(tech);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTechnician([FromQuery] string id, [FromBody] AppUserDTO dto)
        {
            var tech = await _appUserService.UpdateAppUser(id, dto);
            return Ok(tech);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTechnicians()
        {
               var technicians = await _appUserService.GetAllTechnicianUsers();
               return Ok(technicians);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTechniciansWithoutTask()
        {
               var technicians = await _appUserService.GetAllTechnicianUsersWithoutTask();
               return Ok(technicians);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> GetTechniciansById([FromQuery] string TechId)
        {
            var tech = await _appUserService.GetTechnicianById(TechId);
            return Ok(tech);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckEmail([FromQuery] string email)
        {
            var tech = await _appUserService.CheckEmail(email);
            return Ok(tech);
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangePassword([FromQuery] string TechId, [FromBody] ChangePasswordDTO newPassword)
        {
            await _appUserService.ChangePassword(TechId, newPassword);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var user = await _appUserService.Login(login);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = await _appUserService.CreateToken(user);
            return Ok(token);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromQuery] string Token)
        {
            var RefreshToken = await RefreshTokenService.GetRefreshToken(Token);
            
            if (await RefreshTokenService.ValidateRefreshTokenAsync(RefreshToken))
            {
                return Unauthorized("Invalid refresh Token");
            }

            if (await RefreshTokenService.IsRefreshTokenExpiredAsync(RefreshToken))
            {
                return Unauthorized("Refresh token expired");
            }

            await RefreshTokenService.RevokeRefreshTokenAsync(RefreshToken);

            var response = await _appUserService.CreateToken(RefreshToken.User);

            return Ok(response);
        }

    }
}
