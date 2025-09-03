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
    public class ReadQRCodeByFileHandler : IRequestHandler<ReadQRCodeByFileCommand, EquipmentDTO>
    {
        private readonly IEquipmentRepo _equipmentRepo;
        private readonly IBarCodeService _barCodeService;
        private readonly IMapper _mapper;

        public ReadQRCodeByFileHandler(IEquipmentRepo equipmentRepo, IBarCodeService barCodeService,IMapper mapper)
        {
            _barCodeService = barCodeService;
            _equipmentRepo = equipmentRepo;
            _mapper = mapper;
        }

        public async Task<EquipmentDTO> Handle(ReadQRCodeByFileCommand request, CancellationToken cancellationToken)
        {
            var BarCodeId = _barCodeService.ReadQRCodeByFile(request._formFile);

            if (string.IsNullOrWhiteSpace(BarCodeId))
                return null;

            var Equip = await _equipmentRepo.GetEquipmentByBarCodeId(BarCodeId);

            if (Equip == null)
                return null;

            var Dto = _mapper.Map<EquipmentDTO>(Equip);

            return Dto;
        }
    }
}
