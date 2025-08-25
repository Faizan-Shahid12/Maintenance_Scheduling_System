using Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Commands;
using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Settings;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Commands
{
    public class CreateTokenHandler : IRequestHandler<CreateTokenCommand, TokenResponseDTO>
    {
        private readonly IAppUserRepo _repo;
        private readonly TokenSetting _tokenOptions;
        private readonly IMediator _mediator;

        public CreateTokenHandler(IAppUserRepo repo, IOptions<TokenSetting> options, IMediator mediator)
        {
            _repo = repo;
            _tokenOptions = options.Value;
            _mediator = mediator;
        }

        public async Task<TokenResponseDTO> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var user = request.User;

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("FullName", user.FullName)
            };

            var roles = await _repo.GetRoles(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: DateTime.UtcNow.AddMinutes(_tokenOptions.AccessExpiryInMins),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key)),
                    SecurityAlgorithms.HmacSha256
                )
            );

            var refreshToken = await _mediator.Send(new GenerateRefreshTokenCommand(user.Id));

            return new TokenResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                AccessTokenExpiry = token.ValidTo,
                UserId = user.Id,
                Name = user.FullName,
                Roles = roles.ToList()
            };
        }
    }
}

