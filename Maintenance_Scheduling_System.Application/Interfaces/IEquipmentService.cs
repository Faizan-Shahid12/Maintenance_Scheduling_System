using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Interfaces
{
    public interface IEquipmentService
    {
        Task CreateEquipment(CreateEquipmentDTO equipmentDTO);
        Task DeleteEquipment(int EquipId);
        Task UpdateEquipment(EquipmentDTO equipmentDTO);
        Task<List<EquipmentDTO>> GetAllEquipments();
        Task<List<EquipmentDTO>> GetArchivedEquipments();
        Task<List<EquipmentDTO>> GetEquipmentByName(string name);
        Task<EquipmentDTO> GetEquipmentById(int id);
        Task AssignEquipType(int equipId, string type);
        Task AssignWorkShopLocation(int equipId, int workShopId);
        Task ArchiveEquipment(int equipId);
        Task UnArchiveEquipment(int equipId);
    }
}
