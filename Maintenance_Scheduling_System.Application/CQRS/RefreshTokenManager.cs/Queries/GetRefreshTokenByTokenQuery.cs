using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Queries
{
    public class GetRefreshTokenByTokenQuery : IRequest<RefreshToken>
    {
        public string Token { get; set; }

        public GetRefreshTokenByTokenQuery(string token)
        {
            Token = token;
        }
    }
}
