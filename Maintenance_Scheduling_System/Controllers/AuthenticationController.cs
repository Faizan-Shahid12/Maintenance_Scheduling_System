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
            await _appUserService.CreateAppUser(request, "Technician");
            return Ok("Technician created successfully.");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAdmin([FromQuery] AppUserDTO request)
        {
            await _appUserService.CreateAppUser(request, "Admin");
            return Ok("Admin created successfully.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTechnician(string id)
        {
            await _appUserService.DeleteAppUser(id);
            return Ok("User soft-deleted successfully.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTechnician(string id, [FromBody] AppUserDTO dto)
        {
            await _appUserService.UpdateAppUser(id, dto);
            return Ok("User updated successfully.");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTechnicians()
        {
               var technicians = await _appUserService.GetAllTechnicianUsers();
               return Ok(technicians);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] LoginDTO login)
        {
            var user = await _appUserService.Login(login);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = await _appUserService.CreateToken(user);
            return Ok(token);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] string token)
        {
            var RefreshToken = await RefreshTokenService.GetRefreshToken(token);
            
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
