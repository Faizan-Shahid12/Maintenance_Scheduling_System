using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Queries;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Handlers.QueryHandlers
{
    public class GetAllMaintenanceHistoryHandler : IRequestHandler<GetAllMaintenanceHistoryQuery, List<MaintenanceHistoryDTO>>
    {
        private readonly IMaintenanceHistoryRepo _repo;
        private readonly IMapper _mapper;

        public GetAllMaintenanceHistoryHandler(IMaintenanceHistoryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<MaintenanceHistoryDTO>> Handle(GetAllMaintenanceHistoryQuery request, CancellationToken cancellationToken)
        {
            var history = await _repo.GetAllMaintenanceHistory();
            return _mapper.Map<List<MaintenanceHistoryDTO>>(history);
        }
    }
}
