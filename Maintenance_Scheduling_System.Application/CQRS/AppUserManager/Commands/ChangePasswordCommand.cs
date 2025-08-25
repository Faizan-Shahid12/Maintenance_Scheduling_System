using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Commands
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public string TechnicianId { get; }
        public ChangePasswordDTO Password { get; }

        public ChangePasswordCommand(string technicianId, ChangePasswordDTO password)
        {
            TechnicianId = technicianId;
            Password = password;
        }
    }
}
