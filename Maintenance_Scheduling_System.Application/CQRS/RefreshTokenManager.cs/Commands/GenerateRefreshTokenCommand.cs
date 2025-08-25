using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.RefreshTokenManager.cs.Commands
{
    public class GenerateRefreshTokenCommand : IRequest<string>
    {
        public string UserId { get; set; }

        public GenerateRefreshTokenCommand(string userId)
        {
            UserId = userId;
        }
    }
}
