using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Queries
{
    public class GetAllMainTasksQuery : IRequest<List<MainTaskDTO>>
    {
    }
}
