using Maintenance_Scheduling_System.Application.CQRS.BarCodeManager.Queries;
using Maintenance_Scheduling_System.Application.DTO.BarCodeDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.BarCodeManager.Handlers.QueryHandlers
{
    public class GenerateQRCodeHandler : IRequestHandler<GenerateQRCodeQuery, List<BarCodeResultDTO>>
    {
        private readonly IBarCodeService _barCodeService;
        private readonly IEquipmentRepo _equipmentRepository;

        public GenerateQRCodeHandler(IBarCodeService barCodeService, IEquipmentRepo equipmentRepo)
        {
            _barCodeService = barCodeService;
            _equipmentRepository = equipmentRepo;
        }

        public async Task<List<BarCodeResultDTO>> Handle(GenerateQRCodeQuery request, CancellationToken cancellationToken)
        {
            List<BarCodeResultDTO> result = new List<BarCodeResultDTO>();

            foreach (var id in request.EquipmentId)
            {
                var Equip = await _equipmentRepository.GetEquipmentById(id);

                var BarCode = _barCodeService.GenerateQRCode(Equip.BarCodeId);

                BarCodeResultDTO result1 = new BarCodeResultDTO { BarCode = BarCode, EquipmentName = Equip.Name, EquipmentModel = Equip.Model };

                result.Add(result1);
            }     
            
            return result;

        }
    }
}
