using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO;
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
        private readonly IWorkShopLocRepo WorkShopRepository;
        private readonly IMapper mapper;
        private readonly ICurrentUser currentUser;

        public EquipmentService(IEquipmentRepo equipment, IMapper mapper, ICurrentUser currentUser,IWorkShopLocRepo workShopRepository)
        {
            EquipRepository = equipment;
            this.mapper = mapper;
            this.currentUser = currentUser;
            WorkShopRepository = workShopRepository;
        }

        private void AuditModify(Equipment equip)
        {
            equip.LastModifiedAt = DateTime.UtcNow;
            equip.LastModifiedBy = currentUser.Name;
        }

        private EquipmentDTO MapToDTO(Equipment equip)
        {
            var dto = mapper.Map<EquipmentDTO>(equip);
           
            return dto;
        }

        public async Task<EquipmentDTO> CreateEquipment(CreateEquipmentDTO equipmentDTO)
        {
            var equip = mapper.Map<Equipment>(equipmentDTO);
            equip.CreatedBy = currentUser.Name;


            if(equipmentDTO.WorkShopId != null)
                equip.AssignWorkShopLocation((int)equipmentDTO.WorkShopId);

            AuditModify(equip);

            await EquipRepository.CreateNewEquipment(equip);

            return MapToDTO(equip);

        }

        public async Task<EquipmentDTO> DeleteEquipment(int EquipId)
        {
            var equip = await EquipRepository.GetEquipmentById(EquipId);
            
            AuditModify(equip);
            
            equip.IsDeleted = true;
           
            await EquipRepository.DeleteEquipment(equip);

      

            return MapToDTO(equip);
        }

        public async Task<EquipmentDTO> UpdateEquipment(EquipmentDTO equipmentDTO)
        {
            var existingEquip = await EquipRepository.GetEquipmentById(equipmentDTO.EquipmentId);

            existingEquip.Name = equipmentDTO.Name;
            existingEquip.Type = equipmentDTO.Type;
            existingEquip.location = equipmentDTO.location;
            existingEquip.SerialNumber = equipmentDTO.SerialNumber;
            existingEquip.Model = equipmentDTO.Model;
            
            AuditModify(existingEquip);

            await EquipRepository.UpdateEquipment(existingEquip);

            return MapToDTO(existingEquip);
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

        public async Task<EquipmentDTO> AssignEquipType(int EquipId,string Type)
        {
            var equip = await EquipRepository.GetEquipmentById(EquipId);
          
            int check = equip.AssignType(Type);

            if (check == -1)
                return null;

            AuditModify(equip);
            
            await EquipRepository.UpdateEquipment(equip);

            return MapToDTO(equip);
        }

        public async Task<EquipmentDTO> AssignWorkShopLocation(int EquipId, int WorkShopId)
        {
            var equip = await EquipRepository.GetEquipmentById(EquipId);

            if(WorkShopId <=0)
            {
                equip.WorkshopId = null;

                AuditModify(equip);
            
                await EquipRepository.UpdateEquipment(equip);

                return MapToDTO(equip);
            }

            int check = equip.AssignWorkShopLocation(WorkShopId);

            if (check == -1)
                return null;

            AuditModify(equip);
            
            await EquipRepository.UpdateEquipment(equip);

            var equipdto = MapToDTO(equip);

            var workshop = await WorkShopRepository.GetWorkShopById(WorkShopId);

            if(workshop == null)
            {
                return equipdto;
            }

            equipdto.WorkShopLocation = workshop.Location;
           
            equipdto.WorkShopName = workshop.Name;

            return equipdto;
        }

        public async Task<EquipmentDTO> ArchiveEquipment(int EquipId)
        {
            var equip = await EquipRepository.GetEquipmentById(EquipId);
            equip.Archive();

            AuditModify(equip);

            await EquipRepository.UpdateEquipment(equip);

            return MapToDTO(equip);
        }

        public async Task<EquipmentDTO> UnArchiveEquipment(int EquipId)
        {
            var equip = await EquipRepository.GetEquipmentById(EquipId);
            
            equip.UnArchive();
            
            AuditModify(equip);
            
            await EquipRepository.UpdateEquipment(equip);
           
            return MapToDTO(equip);
        }

        public async Task<List<EquipmentDTO>> GetArchivedEquipments()
        {
            var archivedEquip = await EquipRepository.GetAllArchivedEquipment();

            var archivedDtos = mapper.Map<List<EquipmentDTO>>(archivedEquip);

            return archivedDtos;
        }

        public async Task<List<WorkShopDTO>> GetAllWorkShops()
        {
            var list = await WorkShopRepository.GetAllWorkShopLoc();

            var workshopDTO = mapper.Map<List<WorkShopDTO>>(list);

            return workshopDTO;

        }
    }
}
