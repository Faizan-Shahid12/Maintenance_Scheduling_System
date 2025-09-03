using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.BarCodeManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.BarCodeManager.Handlers.CommandHandlers
{
    public class ReadBarCode1Handler : IRequestHandler<ReadQRCodeCommand, EquipmentDTO>
    {
        private readonly IEquipmentRepo _equipmentRepo;
        private readonly IBarCodeService _barCodeService;
        private readonly IMapper _mapper;

        public ReadBarCode1Handler(IEquipmentRepo equipmentRepo, IBarCodeService barCodeService,IMapper mapper)
        {
            _barCodeService = barCodeService;
            _equipmentRepo = equipmentRepo;
            _mapper = mapper;
        }

        public async Task<EquipmentDTO> Handle(ReadQRCodeCommand request, CancellationToken cancellationToken)
        {
            var Equip = await _equipmentRepo.GetEquipmentByBarCodeId(request.BarCodeId);

            if (Equip == null)
                return null;

            var Dto = _mapper.Map<EquipmentDTO>(Equip);

            return Dto;
        }
    }
}
