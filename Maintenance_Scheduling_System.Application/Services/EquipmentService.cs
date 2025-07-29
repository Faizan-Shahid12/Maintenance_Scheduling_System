using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepo EquipRepository;
        private readonly IMapper mapper;
        private ICurrentUser currentUser;

        public EquipmentService(IEquipmentRepo equipment, IMapper mapper, ICurrentUser currentUser)
        {
            EquipRepository = equipment;
            this.mapper = mapper;
            this.currentUser = currentUser;
        }

        private void AuditModify(Equipment equip)
        {
            equip.LastModifiedAt = DateTime.UtcNow;
            equip.LastModifiedBy = currentUser.Name;
        }

        public async Task CreateEquipment(CreateEquipmentDTO equipmentDTO)
        {
            var equip = mapper.Map<Equipment>(equipmentDTO);
            equip.CreatedBy = currentUser.Name;
            AuditModify(equip);
            await EquipRepository.CreateNewEquipment(equip);
        }

        public async Task DeleteEquipment(int EquipId)
        {
            var equip = await EquipRepository.GetEquipmentById(EquipId);
            AuditModify(equip);
            equip.IsDeleted = true;
            await EquipRepository.DeleteEquipment(equip);


        }

        public async Task UpdateEquipment(EquipmentDTO equipmentDTO)
        {
            var existingEquip = await EquipRepository.GetEquipmentById(equipmentDTO.EquipmentId);

            existingEquip.Name = equipmentDTO.Name;
            existingEquip.Type = equipmentDTO.Type;
            existingEquip.location = equipmentDTO.location;
            existingEquip.SerialNumber = equipmentDTO.SerialNumber;
            existingEquip.Model = equipmentDTO.Model;
            AuditModify(existingEquip);

            await EquipRepository.UpdateEquipment(existingEquip);
        }

        public async Task<List<EquipmentDTO>> GetAllEquipments()
        {
            var equip = await EquipRepository.GetAllEquipment();
            var DTOs = mapper.Map<List<EquipmentDTO>>(equip);

            return DTOs;
        }

        public async Task<List<EquipmentDTO>> GetEquipmentByName(string Name)
        {
            var equip = await EquipRepository.GetEquipmentByName(Name);
            var DTOS = mapper.Map<List<EquipmentDTO>>(equip);
            return DTOS;
        }

        public async Task<EquipmentDTO> GetEquipmentById(int id)
        {
            var Dto = mapper.Map<EquipmentDTO>(await EquipRepository.GetEquipmentById(id));
            return Dto;
        }

        public async Task AssignEquipType(int EquipId,string Type)
        {
            var equip = await EquipRepository.GetEquipmentById(EquipId);
            int check = equip.AssignType(Type);

            if (check == -1)
                return;

            AuditModify(equip);
            await EquipRepository.UpdateEquipment(equip);
        }

        public async Task AssignWorkShopLocation(int EquipId, int WorkShopId)
        {
            var equip = await EquipRepository.GetEquipmentById(EquipId);
            int check = equip.AssignWorkShopLocation(WorkShopId);

            if (check == -1)
                return;

            AuditModify(equip);
            await EquipRepository.UpdateEquipment(equip);
        }

        public async Task ArchiveEquipment(int EquipId)
        {
            var equip = await EquipRepository.GetEquipmentById(EquipId);
            equip.Archive();
            AuditModify(equip);
            await EquipRepository.UpdateEquipment(equip);
        }

        public async Task UnArchiveEquipment(int EquipId)
        {
            var equip = await EquipRepository.GetEquipmentById(EquipId);
            equip.UnArchive();
            AuditModify(equip);
            await EquipRepository.UpdateEquipment(equip);
        }

        public async Task<List<EquipmentDTO>> GetArchivedEquipments()
        {
            var equip = await EquipRepository.GetAllEquipment();
            var archivedEquip = equip.Where(x => x.IsArchived == true).ToList();
            var archivedDtos = mapper.Map<List<EquipmentDTO>>(archivedEquip);

            return archivedDtos;
        }
    }
}
