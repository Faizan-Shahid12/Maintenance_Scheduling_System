using Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Queries;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Handlers.QueryHandlers
{
    public class GetRefreshTokenByTokenHandler : IRequestHandler<GetRefreshTokenByTokenQuery, RefreshToken>
    {
        private readonly IRefreshTokenRepo RefreshTokenRepository;

        public GetRefreshTokenByTokenHandler(IRefreshTokenRepo repo)
        {
            RefreshTokenRepository = repo;
        }

        public async Task<RefreshToken> Handle(GetRefreshTokenByTokenQuery request, CancellationToken cancellationToken)
        {
            return await RefreshTokenRepository.GetRefreshTokenByToken(request.Token);
        }
    }
}
