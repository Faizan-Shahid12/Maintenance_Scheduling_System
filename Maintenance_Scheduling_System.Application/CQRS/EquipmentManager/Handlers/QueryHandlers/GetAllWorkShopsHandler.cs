using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Queries;
using Maintenance_Scheduling_System.Application.DTO;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Handlers.QueryHandlers
{
    public class GetAllWorkShopsHandler : IRequestHandler<GetAllWorkShopsQuery, List<WorkShopDTO>>
    {
        private readonly IWorkShopLocRepo workShopRepo;
        private readonly IMapper mapper;

        public GetAllWorkShopsHandler(IWorkShopLocRepo workShopRepo, IMapper mapper)
        {
            this.workShopRepo = workShopRepo;
            this.mapper = mapper;
        }

        public async Task<List<WorkShopDTO>> Handle(GetAllWorkShopsQuery request, CancellationToken cancellationToken)
        {
            var list = await workShopRepo.GetAllWorkShopLoc();
            return mapper.Map<List<WorkShopDTO>>(list);
        }
    }
}
