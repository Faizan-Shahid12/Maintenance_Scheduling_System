using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands
{
    public class UnAssignTechnicianTaskUponDeletionCommand : IRequest
    {
        public string TechId { get; set; }
        public UnAssignTechnicianTaskUponDeletionCommand(string techId) => TechId = techId;
    }
}
