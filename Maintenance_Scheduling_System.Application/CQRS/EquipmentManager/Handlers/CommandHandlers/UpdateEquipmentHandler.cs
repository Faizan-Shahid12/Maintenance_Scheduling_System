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
    public class UpdateEquipmentHandler : IRequestHandler<UpdateEquipmentCommand, EquipmentDTO>
    {
        private readonly IEquipmentRepo EquipRepository;
        private readonly IMapper mapper;

        public UpdateEquipmentHandler(IEquipmentRepo EquipmentRepository, IMapper mapper)
        {
            this.EquipRepository = EquipmentRepository;
            this.mapper = mapper;
        }

        public async Task<EquipmentDTO> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var equipmentDTO = request.EquipmentDTO;
            var existingEquip = await EquipRepository.GetEquipmentById(equipmentDTO.EquipmentId);

            existingEquip.Name = equipmentDTO.Name;
            existingEquip.Type = equipmentDTO.Type;
            existingEquip.location = equipmentDTO.location;
            existingEquip.SerialNumber = equipmentDTO.SerialNumber;
            existingEquip.Model = equipmentDTO.Model;

            await EquipRepository.UpdateEquipment(existingEquip);

            return mapper.Map<EquipmentDTO>(existingEquip);
        }
    }
}
