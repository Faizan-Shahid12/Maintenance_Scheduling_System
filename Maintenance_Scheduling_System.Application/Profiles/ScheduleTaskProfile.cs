using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Services;
using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Profiles
{
    public class ScheduleTaskProfile : Profile
    {
        private readonly IScheduleTaskService _scheduleTaskService;

       
        public ScheduleTaskProfile()
        {
            CreateMap<CreateScheduleTaskDTO, ScheduleTask>()
                .ForMember(st => st.ScheduleId, st => st.Ignore())
                .ForMember(st => st.ScheduleTaskId, st => st.Ignore())
                .ForMember(st => st.schedule, st => st.Ignore());

            CreateMap<ScheduleTaskDTO, ScheduleTask>().ReverseMap();

        }
    }
}
