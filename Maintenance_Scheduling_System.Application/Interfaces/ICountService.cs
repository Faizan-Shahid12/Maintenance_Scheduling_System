using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Interfaces
{
    public interface ICountService
    {
        public Task<List<int>> GetTotalCount();

    }
}
