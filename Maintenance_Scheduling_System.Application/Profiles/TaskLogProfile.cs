using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Profiles
{
    public class TaskLogProfile : Profile
    {
        public TaskLogProfile()
        {
            CreateMap<CreateTaskLogDTO, TaskLogs>()
                .ForMember(t => t.task, t => t.Ignore())
                .ForMember(t => t.TaskId, t => t.Ignore())
                .ForMember(t => t.Attachments, t => t.Ignore());

            CreateMap<TaskLogDTO, TaskLogs>()
                .ForMember(t => t.LCreatedBy, t => t.MapFrom(t => t.CreatedBy))
                .ForMember(t => t.LCreatedAt, t => t.MapFrom(t => t.CreatedAt))
                .ReverseMap();
        }
    }
}
