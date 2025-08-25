using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Commands
{
    public class DeleteAppUserCommand : IRequest<TechnicianDTO>
    {
        public string Id { get; }

        public DeleteAppUserCommand(string id)
        {
            Id = id;
        }
    }
}
