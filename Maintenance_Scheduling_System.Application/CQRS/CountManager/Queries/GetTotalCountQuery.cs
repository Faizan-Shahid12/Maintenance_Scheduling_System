using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.CQRS.CountManager.Queries
{
    public class GetTotalCountQuery : IRequest<List<int>>
    {
    }
}
