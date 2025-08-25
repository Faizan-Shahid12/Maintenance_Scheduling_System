using Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Commands;
using Maintenance_Scheduling_System.Application.Settings;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Handlers.CommandHandlers
{
    public class GenerateRefreshTokenHandler : IRequestHandler<GenerateRefreshTokenCommand, string>
    {
        private readonly IRefreshTokenRepo RefreshTokenRepository;
        private readonly TokenSetting TokenOptions;

        public GenerateRefreshTokenHandler(IRefreshTokenRepo repo, IOptions<TokenSetting> tokenOptions)
        {
            RefreshTokenRepository = repo;
            TokenOptions = tokenOptions.Value;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }
 
        public async Task<string> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string token = GenerateRefreshToken();

            RefreshToken refreshToken = new RefreshToken
            {
                Token = token,
                UserId = request.UserId,
                Expiration = DateTime.UtcNow.AddDays(TokenOptions.RefreshTokenExpiryInDays),
                IsRevoked = false
            };

            await RefreshTokenRepository.AddnewRefreshToken(refreshToken);

            return token;
        }
    }
}
