using Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Commands;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Handlers.CommandHandlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IAppUserRepo AppUserRepository;

        public ChangePasswordCommandHandler(IAppUserRepo repo)
        {
            AppUserRepository = repo;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await AppUserRepository.ChangePassword(request.TechnicianId, request.Password.newPassword);
            return Unit.Value;
        }
    }
}
