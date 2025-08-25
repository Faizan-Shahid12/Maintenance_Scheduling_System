using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Queries;
using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Handlers.QueryHandlers
{
    public class GetAllTechniciansHandler : IRequestHandler<GetAllTechniciansQuery, List<TechnicianDTO>>
    {
        private readonly IAppUserRepo _repo;
        private readonly IMapper _mapper;

        public GetAllTechniciansHandler(IAppUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<TechnicianDTO>> Handle(GetAllTechniciansQuery request, CancellationToken cancellationToken)
        {
            var users = await _repo.GetTechniciansUsers();
            return _mapper.Map<List<TechnicianDTO>>(users);
        }
    }
}
