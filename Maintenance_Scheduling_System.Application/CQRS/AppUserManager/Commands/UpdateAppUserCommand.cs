using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Commands
{
    public class UpdateAppUserCommand : IRequest<TechnicianDTO>
    {
        public string Id { get; }
        public AppUserDTO AppUser { get; }

        public UpdateAppUserCommand(string id, AppUserDTO appUser)
        {
            Id = id;
            AppUser = appUser;
        }
    }
}
