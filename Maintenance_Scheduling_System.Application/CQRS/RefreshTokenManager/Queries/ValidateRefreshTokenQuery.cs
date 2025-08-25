using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Queries
{
    public class ValidateRefreshTokenQuery : IRequest<bool>
    {
        public RefreshToken RefreshToken { get; set; }

        public ValidateRefreshTokenQuery(RefreshToken refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
