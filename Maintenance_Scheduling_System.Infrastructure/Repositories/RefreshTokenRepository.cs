using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Maintenance_Scheduling_System.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepo
    {
        private readonly Maintenance_DbContext Dbcontext;

        public RefreshTokenRepository(Maintenance_DbContext context)
        {
            Dbcontext = context;
        }
        public async Task AddnewRefreshToken(RefreshToken refreshToken)
        {
            await Dbcontext.RefreshTokens.AddAsync(refreshToken);
            await Dbcontext.SaveChangesAsync();
        }

        public async Task DeleteRefreshToken()
        {
            await Dbcontext.SaveChangesAsync();
        }

        public async Task<List<RefreshToken>> GetAllRefreshTokenById(string id)
        {
            var token = await Dbcontext.RefreshTokens.Where(x => x.UserId == id).ToListAsync();
            return token;
        }
        public async Task<RefreshToken> GetRefreshTokenByUserIdAndToken(string id,string token)
        {
            var retoken = await Dbcontext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == id && x.Token == token);
            return retoken;
        }
        public async Task<RefreshToken> GetRefreshTokenByToken(string token)
        {
            var retoken = await Dbcontext.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(x => x.Token == token);
            return retoken;
        }
    }
}
