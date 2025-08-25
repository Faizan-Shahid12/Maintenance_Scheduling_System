using Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Handlers.QueryHandlers
{
    public class IsRefreshTokenExpiredHandler : IRequestHandler<IsRefreshTokenExpiredQuery, bool>
    {
        public async Task<bool> Handle(IsRefreshTokenExpiredQuery request, CancellationToken cancellationToken)
        {
            return request.RefreshToken.Expiration < DateTime.UtcNow;
        }
    }
}
