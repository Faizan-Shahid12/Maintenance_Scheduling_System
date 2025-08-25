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
    public class DeleteAppUserHandler : IRequestHandler<DeleteAppUserCommand, TechnicianDTO>
    {
        private readonly IAppUserRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IMainTaskService _mainTaskService;

        public DeleteAppUserHandler(IAppUserRepo repo, IMapper mapper, ICurrentUser currentUser, IMainTaskService mainTaskService)
        {
            _repo = repo;
            _mapper = mapper;
            _currentUser = currentUser;
            _mainTaskService = mainTaskService;
        }

        public async Task<TechnicianDTO> Handle(DeleteAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetAppUserById(request.Id);
            if (user == null) return null;

            user.IsDeleted = true;

            await _repo.DeleteAppUser(user);
            await _mainTaskService.UnAssignTechnicianTaskUponDeletion(request.Id);

            return _mapper.Map<TechnicianDTO>(user);
        }
    }
}
