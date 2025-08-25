using Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Handlers.QueryHandlers
{
    public class ValidateRefreshTokenHandler : IRequestHandler<ValidateRefreshTokenQuery, bool>
    {
        public async Task<bool> Handle(ValidateRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            if (request.RefreshToken == null || request.RefreshToken.IsRevoked)
            {
                return false;
            }
            return true; 
        }
    }
}
