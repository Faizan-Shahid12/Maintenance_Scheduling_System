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
    public class RevokeRefreshTokenHandler : IRequestHandler<RevokeRefreshTokenCommand>
    {
        private readonly IRefreshTokenRepo RefreshTokenRepository;
        public RevokeRefreshTokenHandler(IRefreshTokenRepo repo)
        {
            RefreshTokenRepository = repo;
        }
        public async Task Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            request.RefreshToken.IsRevoked = true;

            await RefreshTokenRepository.DeleteRefreshToken();

            return;
        }
    }
}
