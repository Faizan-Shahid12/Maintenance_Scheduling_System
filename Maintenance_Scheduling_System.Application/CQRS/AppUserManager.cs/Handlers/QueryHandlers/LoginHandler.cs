using Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Queries;
using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Handlers.QueryHandlers
{
    public class LoginHandler : IRequestHandler<LoginQuery, AppUser>
    {
        private readonly IAppUserRepo _repo;

        public LoginHandler(IAppUserRepo repo)
        {
            _repo = repo;
        }

        public async Task<AppUser> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetAppUserByEmail(request.LoginInfo.Email);
            if (user == null) return null;

            var hasher = new PasswordHasher<AppUser>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, request.LoginInfo.Password);

            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
