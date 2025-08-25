using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
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
    public class MaintenanceHistoryService : IMaintenanceHistoryService
    {
        //DONE

        private IMaintenanceHistoryRepo MaintenanceHistoryRepository {  get; set; }
        private IMapper mapper { get; set; }
        private ICurrentUser currentUser { get; set; }
        private IEquipmentRepo EquipmentRepository { get; set; }

        public MaintenanceHistoryService(IMaintenanceHistoryRepo maintenanceHistoryRepository,IMapper mapper,ICurrentUser currentUser,IEquipmentRepo equipmentRepository,IMainTaskRepo mainTaskRepository)
        {
            MaintenanceHistoryRepository = maintenanceHistoryRepository;
            this.mapper = mapper;
            this.currentUser = currentUser;
            EquipmentRepository = equipmentRepository;
        }

        private void AuditModify(MaintenanceHistory equip)
        {
            equip.LastModifiedAt = DateTime.UtcNow;
            equip.LastModifiedBy = currentUser.Name;
        }

        public async Task CreateMaintenanceHistoryFromTask(CreateMaintenanceHistoryDTO mainhisDTO,MainTask task,Equipment equip)
        {
            
            var mainhis = mapper.Map<MaintenanceHistory>(mainhisDTO);

            mainhis.AddTask(task);

            mainhis.CreatedBy = currentUser.Name;

            AuditModify(mainhis);
            RecalculateEndDate(mainhis);

            equip.LastModifiedAt = DateTime.Now;
            equip.LastModifiedBy = currentUser.Name;
            equip.AddMaintenance(mainhis);

            await MaintenanceHistoryRepository.CreateNewMaintenanceHistory(mainhis);

        }

        public async Task AddNewTasktoHistory(int id,MainTask Task)
        {
            var main = await MaintenanceHistoryRepository.GetMaintenanceHistory(id);
            main.AddTask(Task);
            RecalculateEndDate(main);

            AuditModify(main);

            await MaintenanceHistoryRepository.AddTask();
        }

        public async Task EditHistory(MaintenanceHistoryDTO mainDto)
        {
            var maindto = mapper.Map<MaintenanceHistory>(mainDto); 

            var main1 = await MaintenanceHistoryRepository.GetMaintenanceHistory(maindto.HistoryId);

            if (main1 == null) throw new Exception("History Does not Exist");

            main1.EquipmentName = maindto.EquipmentName;
            main1.EquipmentType = maindto.EquipmentType;
            main1.StartDate = maindto.StartDate;
            main1.EndDate = maindto.EndDate;
            RecalculateEndDate(main1);
            AuditModify(main1);

            await MaintenanceHistoryRepository.UpdateMaintenanceHistory();
        }

        public async Task DeleteHistory(int HistoryId)
        {
            var main = await MaintenanceHistoryRepository.GetMaintenanceHistory(HistoryId);
            main.IsDeleted = true;
            AuditModify(main);

            await MaintenanceHistoryRepository.UpdateMaintenanceHistory();
        }

        public async Task<List<MaintenanceHistoryDTO>> GetMaintenanceHistoryByEquipmentId(int EquipmentId)
        {
            var main = await MaintenanceHistoryRepository.GetMaintenanceHistoryByEquipId(EquipmentId);
            var mainDTO = mapper.Map<List<MaintenanceHistoryDTO>>(main);
            return mainDTO;
        }
        public async Task<List<MaintenanceHistoryDTO>> GetMaintenanceHistoryByEquipId(int EquipmentId)
        {
            var main = await MaintenanceHistoryRepository.GetMaintenanceHistoryByEquipId(EquipmentId);
            var mainDTO = mapper.Map<List<  MaintenanceHistoryDTO>>(main);
            return mainDTO;
        }

        public async Task<List<MaintenanceHistoryDTO>> GetAllMaintenanceHistory()
        {
            var main = await MaintenanceHistoryRepository.GetAllMaintenanceHistory();
            var mainDTO = mapper.Map<List<MaintenanceHistoryDTO>>(main);
            return mainDTO;
        }
        private void RecalculateEndDate(MaintenanceHistory history)
        {
            if (history.tasks != null && history.tasks.Any())
            {
                history.EndDate = history.tasks
                    .Where(t => t.DueDate != null)
                    .Max(t => t.DueDate); 
            }
        }


    }
}
