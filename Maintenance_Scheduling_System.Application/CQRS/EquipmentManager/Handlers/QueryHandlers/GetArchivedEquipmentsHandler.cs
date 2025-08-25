using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Queries;
using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Handlers.QueryHandlers
{
    public class GetArchivedEquipmentsHandler : IRequestHandler<GetArchivedEquipmentsQuery, List<EquipmentDTO>>
    {
        private readonly IEquipmentRepo equipmentRepo;
        private readonly IMapper mapper;

        public GetArchivedEquipmentsHandler(IEquipmentRepo equipmentRepo, IMapper mapper)
        {
            this.equipmentRepo = equipmentRepo;
            this.mapper = mapper;
        }

        public async Task<List<EquipmentDTO>> Handle(GetArchivedEquipmentsQuery request, CancellationToken cancellationToken)
        {
            var equip = await equipmentRepo.GetAllArchivedEquipment();
            return mapper.Map<List<EquipmentDTO>>(equip);
        }
    }
}
