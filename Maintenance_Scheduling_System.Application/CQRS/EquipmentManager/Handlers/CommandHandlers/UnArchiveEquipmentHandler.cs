using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Handlers.CommandHandlers
{
    public class UnArchiveEquipmentHandler : IRequestHandler<UnArchiveEquipmentCommand, EquipmentDTO>
    {
        private readonly IEquipmentRepo equipmentRepo;
        private readonly IMapper mapper;

        public UnArchiveEquipmentHandler(IEquipmentRepo equipmentRepo, IMapper mapper)
        {
            this.equipmentRepo = equipmentRepo;
            this.mapper = mapper;
        }

        public async Task<EquipmentDTO> Handle(UnArchiveEquipmentCommand request, CancellationToken cancellationToken)
        {
            var equip = await equipmentRepo.GetEquipmentById(request.EquipmentId);

            equip.UnArchive();

            await equipmentRepo.UpdateEquipment(equip);

            return mapper.Map<EquipmentDTO>(equip);
        }
    }
}
