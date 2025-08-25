using Maintenance_Scheduling_System.Application.DTO.AppUserDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.AppUserManager.cs.Queries
{
    public class GetTechnicianByIdQuery : IRequest<TechnicianDTO>
    {
        public string TechnicianId { get; }

        public GetTechnicianByIdQuery(string technicianId)
        {
            TechnicianId = technicianId;
        }
    }
}
