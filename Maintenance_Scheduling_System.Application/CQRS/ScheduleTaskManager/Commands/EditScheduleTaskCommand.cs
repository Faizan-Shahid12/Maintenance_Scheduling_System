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
    public class EditScheduleTaskCommand : IRequest<ScheduleTaskDTO>
    {
        public ScheduleTask Task { get; }
        public ScheduleTaskDTO STDTO { get; }

        public EditScheduleTaskCommand(ScheduleTask task, ScheduleTaskDTO stdto)
        {
            Task = task;
            STDTO = stdto;
        }
    }
}
