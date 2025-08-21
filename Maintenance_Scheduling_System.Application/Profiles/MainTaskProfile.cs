using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Profiles
{
    public class MainTaskProfile : Profile
    {
        public MainTaskProfile()
        {
            CreateMap<CreateMainTaskDTO, MainTask>()
                .ForMember(M => M.Equipment, M => M.Ignore())
                .ForMember(M => M.EquipmentId, M => M.Ignore())
                .ForMember(M => M.History, M => M.Ignore())
                .ForMember(M => M.HistoryId, M => M.Ignore());

            CreateMap<MainTaskDTO, MainTask>()
    .ForMember(dest => dest.Technician, opt => opt.Ignore()); // Ignore full Technician mapping

            CreateMap<MainTask, MainTaskDTO>()
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src =>
                    src.Technician != null && !string.IsNullOrEmpty(src.Technician.FullName)
                        ? src.Technician.FullName
                        : "N/A"))
                 .ForMember(dest => dest.TechEmail, opt => opt.MapFrom(src =>
                    src.Technician != null && !string.IsNullOrEmpty(src.Technician.Email)
                        ? src.Technician.Email
                        : "N/A"));

        }
    }
}
