using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Queries
{
    public class LoginQuery : IRequest<AppUser>
    {
        public LoginDTO LoginInfo { get; }

        public LoginQuery(LoginDTO loginInfo)
        {
            LoginInfo = loginInfo;
        }
    }
}
