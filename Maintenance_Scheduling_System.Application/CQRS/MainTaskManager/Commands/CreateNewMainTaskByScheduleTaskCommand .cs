using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands
{
    public class CreateNewMainTaskByScheduleTaskCommand : IRequest<List<MainTaskDTO>>
    {
        public int EquipId { get; set; }
        public List<ScheduleTask> ScheduleTasks { get; set; }

        public CreateNewMainTaskByScheduleTaskCommand(int equipId, List<ScheduleTask> scheduleTasks)
        {
            EquipId = equipId;
            ScheduleTasks = scheduleTasks;
        }
    }
}
