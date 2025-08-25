using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Handlers.CommandHandlers
{
    public class CreateAppUserHandler : IRequestHandler<CreateAppUserCommand, TechnicianDTO>
    {
        private readonly IAppUserRepo _repo;
        private readonly IMapper _mapper;

        public CreateAppUserHandler(IAppUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TechnicianDTO> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = new AppUser
            {
                FullName = request.AppUser.FullName,
                UserName = request.AppUser.FullName.Replace(" ", "") + Guid.NewGuid(),
                Email = request.AppUser.Email,
                PhoneNumber = request.AppUser.PhoneNumber,
                Address = request.AppUser.Address,
                Gender = request.AppUser.Gender,
            };

            await _repo.CreateNewAppUser(appUser, request.AppUser.Password, request.Role);
            return _mapper.Map<TechnicianDTO>(appUser);
        }
    }
}
