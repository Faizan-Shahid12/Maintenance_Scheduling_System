using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Commands
{
    public class CreateTokenCommand : IRequest<TokenResponseDTO>
    {
        public AppUser User { get; }

        public CreateTokenCommand(AppUser user)
        {
            User = user;
        }
    }
}
