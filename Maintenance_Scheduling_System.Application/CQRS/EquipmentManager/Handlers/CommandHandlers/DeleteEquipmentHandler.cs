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
    public class DeleteEquipmentHandler : IRequestHandler<DeleteEquipmentCommand, EquipmentDTO>
    {
        private readonly IEquipmentRepo EquipRepository;
        private readonly IMapper mapper;

        public DeleteEquipmentHandler(IEquipmentRepo EquipmentRepository, IMapper mapper)
        {
            this.EquipRepository = EquipmentRepository;
            this.mapper = mapper;
        }
        public async Task<EquipmentDTO> Handle(DeleteEquipmentCommand request, CancellationToken cancellationToken)
        {
            var equip = await EquipRepository.GetEquipmentById(request.EquipId);

            equip.IsDeleted = true;

            await EquipRepository.DeleteEquipment(equip);

            return mapper.Map<EquipmentDTO>(equip);
        }
    }
    
}
