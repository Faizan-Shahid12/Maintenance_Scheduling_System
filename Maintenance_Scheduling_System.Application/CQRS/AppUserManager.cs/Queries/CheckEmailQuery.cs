using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Queries
{
    public class CheckEmailQuery : IRequest<bool>
    {
        public string NewEmail { get; }

        public CheckEmailQuery(string newEmail)
        {
            NewEmail = newEmail;
        }
    }
}
