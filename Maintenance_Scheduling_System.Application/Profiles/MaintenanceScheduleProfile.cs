using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Profiles
{
    public class MaintenanceScheduleProfile : Profile
    {
        public MaintenanceScheduleProfile()
        {
            CreateMap<CreateMaintenanceScheduleDTO, MaintenanceSchedule>()
                .ForMember(ms => ms.EquipmentId, ms => ms.Ignore())
                .ForMember(ms => ms.ScheduleId, ms => ms.Ignore())
                .ForMember(ms => ms.equipment, ms => ms.Ignore())
                .ForMember(ms => ms.ScheduleTasks, ms => ms.Ignore());

            CreateMap<MaintenanceScheduleDTO, MaintenanceSchedule>().ReverseMap();

            CreateMap<DisplayMaintenanceScheduleDTO, MaintenanceSchedule>().ReverseMap()
                .ForMember(ms => ms.EquipmentName, ms => ms.MapFrom(ms => ms.equipment.Name))
                .ForMember(dest => dest.ScheduleTasks, opt => opt.MapFrom(src => src.ScheduleTasks));

        }
    }
}
