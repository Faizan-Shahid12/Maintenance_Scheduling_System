using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.IRepo
{
    public interface IRefreshTokenRepo
    {
        Task AddnewRefreshToken(RefreshToken refreshToken);
        Task<List<RefreshToken>> GetAllRefreshTokenById(string id);
        Task<RefreshToken> GetRefreshTokenByUserIdAndToken(string id, string token);
        Task<RefreshToken> GetRefreshTokenByToken(string token);
        Task DeleteRefreshToken();
    }
}
