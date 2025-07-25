using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Domain.IRepo
{
    public interface IEquipmentRepo
    {
        public Task CreateNewEquipment(Equipment equip);
        public Task DeleteEquipment(Equipment equip);
        public Task UpdateEquipment(Equipment equip);
        public Task<Equipment> GetEquipment(string Name);
        public Task<List<Equipment>> GetAllEquipment();
        public Task<Equipment> GetEquipmentByName(string Name);
    }
}
