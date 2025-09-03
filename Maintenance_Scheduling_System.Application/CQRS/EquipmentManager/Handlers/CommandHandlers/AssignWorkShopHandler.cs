using AutoMapper;
using Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Handlers.CommandHandlers
{
    public class AssignWorkShopHandler : IRequestHandler<AssignWorkshopCommand, EquipmentDTO>
    {
        private readonly IEquipmentRepo equipmentRepo;
        private readonly IWorkShopLocRepo workShopRepo;
        private readonly IMapper mapper;

        public AssignWorkShopHandler(IEquipmentRepo equipmentRepo, IWorkShopLocRepo workShopRepo, IMapper mapper)
        {
            this.equipmentRepo = equipmentRepo;
            this.workShopRepo = workShopRepo;
            this.mapper = mapper;
        }

        public async Task<EquipmentDTO> Handle(AssignWorkshopCommand request, CancellationToken cancellationToken)
        {
            var workshop = await workShopRepo.GetWorkShopByLocation(request.WorkShop.Location);

            if (workshop == null)
            {
                workshop = mapper.Map<WorkShopLoc>(request.WorkShop);

                await workShopRepo.AddNewWorkShop(workshop);
            }

            var equip = await equipmentRepo.GetEquipmentById(request.EquipmentId);

            //var workshop = await workShopRepo.GetWorkShopById(request.WorkShop.WorkShopId);

            if (workshop.WorkShopId <= 0)
            {
                equip.WorkshopId = null;

                await equipmentRepo.UpdateEquipment(equip);

                return mapper.Map<EquipmentDTO>(equip);
            }

            int check = equip.AssignWorkShopLocation(workshop.WorkShopId);

            if (check == -1) return null;

            await equipmentRepo.UpdateEquipment(equip);

            var equipDto = mapper.Map<EquipmentDTO>(equip);

            if (workshop != null)
            {
                equipDto.WorkShopLocation = workshop.Location;
                equipDto.WorkShopName = workshop.Name;
                equipDto.Latitude = workshop.Latitude;
                equipDto.Longitude = workshop.Longitude;
            }

            return equipDto;
        }
    }
}
