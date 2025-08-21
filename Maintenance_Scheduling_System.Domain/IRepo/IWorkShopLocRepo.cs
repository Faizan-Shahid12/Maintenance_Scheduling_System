using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.IRepo
{
    public interface IWorkShopLocRepo
    {
       public Task<List<WorkShopLoc>> GetAllWorkShopLoc();
        public Task<WorkShopLoc> GetWorkShopById(int workshopId);


    }
}
