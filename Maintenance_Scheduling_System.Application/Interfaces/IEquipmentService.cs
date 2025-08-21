using Maintenance_Scheduling_System.Application.DTO;
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
        Task<EquipmentDTO> CreateEquipment(CreateEquipmentDTO equipmentDTO);
        Task<EquipmentDTO> DeleteEquipment(int EquipId);
        Task<EquipmentDTO> UpdateEquipment(EquipmentDTO equipmentDTO);
        Task<List<EquipmentDTO>> GetAllEquipments();
        Task<List<EquipmentDTO>> GetEquipmentByName(string Name);
        Task<EquipmentDTO> GetEquipmentById(int id);
        Task<EquipmentDTO> AssignEquipType(int EquipId, string Type);
        Task<EquipmentDTO> AssignWorkShopLocation(int EquipId, int WorkShopId);
        Task<EquipmentDTO> ArchiveEquipment(int EquipId);
        Task<EquipmentDTO> UnArchiveEquipment(int EquipId);
        Task<List<EquipmentDTO>> GetArchivedEquipments();
        Task<List<WorkShopDTO>> GetAllWorkShops();

    }
}
