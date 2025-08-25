using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.IRepo;
using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Handlers.CommandHandlers
{
    public class CreateEquipmentHandler : IRequestHandler<CreateEquipmentCommand, EquipmentDTO>
    {
        private readonly IEquipmentRepo EquipRepository;
        private readonly IMapper mapper;

        public CreateEquipmentHandler(IEquipmentRepo EquipmentRepository, IMapper mapper)
        {
            this.EquipRepository = EquipmentRepository;
            this.mapper = mapper;
        }
        public async Task<EquipmentDTO> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var equip = mapper.Map<Equipment>(request.EquipmentDTO);

            if (request.EquipmentDTO.WorkShopId != null)
                equip.AssignWorkShopLocation((int)(request.EquipmentDTO.WorkShopId));

            await EquipRepository.CreateNewEquipment(equip);

            return mapper.Map<EquipmentDTO>(equip);
        }
    }
}
