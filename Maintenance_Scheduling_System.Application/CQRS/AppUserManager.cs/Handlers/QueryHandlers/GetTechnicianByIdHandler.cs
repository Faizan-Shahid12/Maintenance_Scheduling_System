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
    public class GetTechnicianByIdHandler : IRequestHandler<GetTechnicianByIdQuery, TechnicianDTO>
    {
        private readonly IAppUserRepo _repo;
        private readonly IMapper _mapper;

        public GetTechnicianByIdHandler(IAppUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TechnicianDTO> Handle(GetTechnicianByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetAppUserById(request.TechnicianId);
            return user == null ? null : _mapper.Map<TechnicianDTO>(user);
        }
    }
}
