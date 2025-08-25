using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands
{
    public class AssignTechnicianCommand : IRequest<ScheduleTaskDTO>
    {
        public ScheduleTask Task { get; }
        public string? TechId { get; }

        public AssignTechnicianCommand(ScheduleTask task, string? techId)
        {
            Task = task;
            TechId = techId;
        }
    }
}
