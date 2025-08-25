using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.AppUserManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Commands
{
    public class UpdateAppUserHandler : IRequestHandler<UpdateAppUserCommand, TechnicianDTO>
    {
        private readonly IAppUserRepo _repo;
        private readonly IMapper _mapper;

        public UpdateAppUserHandler(IAppUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TechnicianDTO> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetAppUserById(request.Id);
            if (user == null) return null;

            user.FullName = request.AppUser.FullName;
            user.Email = request.AppUser.Email;
            user.PhoneNumber = request.AppUser.PhoneNumber;
            user.Address = request.AppUser.Address;
            user.Gender = request.AppUser.Gender;
            user.UserName = request.AppUser.FullName.Replace(" ", "");


            await _repo.UpdateAppUser(user);

            return _mapper.Map<TechnicianDTO>(user);
        }
    }
}
