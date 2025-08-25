using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Settings;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Services
{
    //DONE
    public class RefreshTokenService : IRefreshTokenService
    {
        public IRefreshTokenRepo RefreshTokenRepository { get; set; }

        public TokenSetting TokenOptions { get; set; }

        public RefreshTokenService(IRefreshTokenRepo refreshTokenRepository, IOptions<TokenSetting> tokenSetting)
        {
            RefreshTokenRepository = refreshTokenRepository;
            TokenOptions = tokenSetting.Value;
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

        public async Task<string> GenerateRefreshTokenAsync(string userId)
        {
            string token = GenerateRefreshToken();
            RefreshToken refreshToken = new RefreshToken
            {
                Token = token,
                UserId = userId,
                Expiration = DateTime.UtcNow.AddDays(TokenOptions.RefreshTokenExpiryInDays),
                IsRevoked = false
            };
            await RefreshTokenRepository.AddnewRefreshToken(refreshToken);

            return token;
        }

        public async Task<bool> IsRefreshTokenExpiredAsync(RefreshToken refreshToken)
        {
            if (refreshToken.Expiration < DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }

        public async Task RevokeRefreshTokenAsync(RefreshToken refreshToken)
        {
            refreshToken.IsRevoked = true;

            await RefreshTokenRepository.DeleteRefreshToken();

            return;
        }

        public async Task<bool> ValidateRefreshTokenAsync(RefreshToken refreshToken)
        {

            if (refreshToken == null || refreshToken.IsRevoked)
            {
                return true;
            }

            return false;

        }

        public async Task<RefreshToken> GetRefreshToken(string token)
        {
            return await RefreshTokenRepository.GetRefreshTokenByToken(token);
        }
    }
}
