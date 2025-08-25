using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Commands
{
    public class CreateAppUserCommand : IRequest<TechnicianDTO>
    {
        public AppUserDTO AppUser { get; }
        public string Role { get; }

        public CreateAppUserCommand(AppUserDTO appUser, string role)
        {
            AppUser = appUser;
            Role = role;
        }
    }
}
