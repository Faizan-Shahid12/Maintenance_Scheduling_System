using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands
{
    public class CreateNewMainTaskCommand : IRequest<MainTaskDTO>
    {
        public int EquipId { get; set; }
        public CreateMainTaskDTO Maintask { get; set; }

        public CreateNewMainTaskCommand(int equipId, CreateMainTaskDTO maintask)
        {
            EquipId = equipId;
            Maintask = maintask;
        }
    }
}
