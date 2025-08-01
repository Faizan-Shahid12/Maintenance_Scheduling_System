using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();
        Task<string> GenerateRefreshTokenAsync(string userId);
        Task<bool> IsRefreshTokenExpiredAsync(RefreshToken token);
        Task RevokeRefreshTokenAsync(RefreshToken token);
        Task<bool> ValidateRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken> GetRefreshToken(string token);
    }
}
