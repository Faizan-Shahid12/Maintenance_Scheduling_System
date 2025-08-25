using Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Commands;
using Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Queries;
using Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Commands;
using Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Queries;
using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTechnician([FromBody] AppUserDTO request)
        {
            var result = await _mediator.Send(new CreateAppUserCommand(request, "Technician"));
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAdmin([FromBody] AppUserDTO request)
        {
            var result = await _mediator.Send(new CreateAppUserCommand(request, "Admin"));
            return Ok("Admin created successfully.");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTechnician([FromQuery] string id)
        {
            var result = await _mediator.Send(new DeleteAppUserCommand(id));
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTechnician([FromQuery] string id, [FromBody] AppUserDTO dto)
        {
            var result = await _mediator.Send(new UpdateAppUserCommand(id, dto));
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTechnicians()
        {
            var result = await _mediator.Send(new GetAllTechniciansQuery());
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTechniciansWithoutTask()
        {
            var result = await _mediator.Send(new GetAllTechniciansWithoutTaskQuery());
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> GetTechniciansById([FromQuery] string techId)
        {
            var result = await _mediator.Send(new GetTechnicianByIdQuery(techId));
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckEmail([FromQuery] string email)
        {
            var result = await _mediator.Send(new CheckEmailQuery(email));
            return Ok(result);
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangePassword([FromQuery] string techId, [FromBody] ChangePasswordDTO newPassword)
        {
            await _mediator.Send(new ChangePasswordCommand(techId, newPassword));
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var user = await _mediator.Send(new LoginQuery(login));

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = await _mediator.Send(new CreateTokenCommand(user));
            return Ok(token);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromQuery] string token)
        {
            var refreshToken = await _mediator.Send(new GetRefreshTokenByTokenQuery(token));
            if (refreshToken == null)
            {
                return Unauthorized("Refresh token not found");
            }

            var isValid = await _mediator.Send(new ValidateRefreshTokenQuery(refreshToken));
            if (!isValid)
            {
                return Unauthorized("Invalid refresh token");
            }

            var isExpired = await _mediator.Send(new IsRefreshTokenExpiredQuery(refreshToken));
            if (isExpired)
            {
                return Unauthorized("Refresh token expired");
            }

            await _mediator.Send(new RevokeRefreshTokenCommand(refreshToken));

            var response = await _mediator.Send(new CreateTokenCommand(refreshToken.User));


            return Ok(response);
        }

    }
}
