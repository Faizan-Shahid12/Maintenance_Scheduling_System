using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Profiles
{
    public class MaintenanceHistoryProfile : Profile
    {
        public MaintenanceHistoryProfile()
        {
            CreateMap<CreateMaintenanceHistoryDTO, MaintenanceHistory>()
                .ForMember(mhdto => mhdto.EquipmentId, mh => mh.Ignore())
                .ForMember(mhdto => mhdto.equipment, mh => mh.Ignore())
                .ForMember(mhdto => mhdto.tasks, mh => mh.Ignore());

            CreateMap<DisplayMaintenanceHistoryDTO, MaintenanceHistory>().ReverseMap();
            CreateMap<EditMaintenanceHistoryDTO, MaintenanceHistory>().ReverseMap();
        }
    }
}
