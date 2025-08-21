using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO;
using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Profiles
{
    public class WorkShopProfile : Profile
    {
        public WorkShopProfile()
        {
            CreateMap<WorkShopDTO, WorkShopLoc>().ReverseMap();
        }
    }
}
