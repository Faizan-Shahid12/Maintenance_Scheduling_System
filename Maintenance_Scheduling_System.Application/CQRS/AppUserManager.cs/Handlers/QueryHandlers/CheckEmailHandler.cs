using Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Queries;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Handlers.QueryHandlers
{
    public class CheckEmailHandler : IRequestHandler<CheckEmailQuery, bool>
    {
        private readonly IAppUserRepo _repo;

        public CheckEmailHandler(IAppUserRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(CheckEmailQuery request, CancellationToken cancellationToken)
        {
            return await _repo.CheckEmail(request.NewEmail);
        }
    }
}
