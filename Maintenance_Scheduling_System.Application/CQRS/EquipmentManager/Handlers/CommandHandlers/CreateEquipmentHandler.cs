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
        private readonly IMediator _mediator;
        private readonly IMapper mapper;

        public CreateEquipmentHandler(IEquipmentRepo EquipmentRepository, IMapper mapper, IMediator mediator)
        {
            this.EquipRepository = EquipmentRepository;
            this.mapper = mapper;
            _mediator = mediator;
        }
        public async Task<EquipmentDTO> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var equip = mapper.Map<Equipment>(request.EquipmentDTO);

            await EquipRepository.CreateNewEquipment(equip);

            if (request.EquipmentDTO.WorkShop != null)
                await _mediator.Send(new AssignWorkshopCommand(equip.EquipmentId,request.EquipmentDTO.WorkShop));

            return mapper.Map<EquipmentDTO>(equip);
        }
    }
}
