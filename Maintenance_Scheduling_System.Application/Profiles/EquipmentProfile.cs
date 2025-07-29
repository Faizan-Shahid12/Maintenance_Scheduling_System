using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using Maintenance_Scheduling_System.Domain.Entities;

namespace Maintenance_Scheduling_System.Application.Profiles
{
    public class EquipmentProfile : Profile
    {
        public EquipmentProfile()
        {
            CreateMap<Equipment, EquipmentDTO>()
                .ForMember(equip => equip.WorkShopLocation, equip => equip.MapFrom(equip => equip.WorkShopLocation != null ? equip.WorkShopLocation.Location : "N/A"))
                .ForMember(equip => equip.WorkShopName, equip => equip.MapFrom(equip => equip.WorkShopLocation != null ? equip.WorkShopLocation.Name : "N/A"));

            CreateMap<CreateEquipmentDTO, Equipment>()
                .ForMember(equip => equip.WorkShopLocation, equip => equip.Ignore())
                .ForMember(equip => equip.WorkshopId, equip => equip.Ignore())
                .ForMember(equip => equip.EquipmentId, equip => equip.Ignore());
        }
    }
}
