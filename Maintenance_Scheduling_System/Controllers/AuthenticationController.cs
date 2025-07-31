using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AuthenticationController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
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

        [Route("api/test-authorize")]
        [HttpGet]
        
        public IActionResult TestAuth()
        {
            return Ok("You are authorized!");
        }

    }
}
