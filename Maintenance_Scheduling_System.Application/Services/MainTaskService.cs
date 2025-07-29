using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
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
    public class MainTaskService : IMainTaskService
    {
        public IMapper mapper {  get; set; }
        public IEquipmentRepo EquipmentReposity { get; set; }
        public IMainTaskRepo MainTaskRepository { get; set; }
        public IMaintenanceHistoryService MaintenanceHistoryService { get; set; }
        public ICurrentUser currentUser { get; set; }

        public MainTaskService(IMapper mapper1,IEquipmentRepo Erepo, IMaintenanceHistoryService MHS,ICurrentUser cu,IMainTaskRepo task)
        {
            mapper = mapper1;
            EquipmentReposity = Erepo;
            MaintenanceHistoryService = MHS;
            currentUser = cu;
            MainTaskRepository = task;
        }

        private void AuditModify(MainTask equip)
        {
            equip.LastModifiedAt = DateTime.UtcNow;
            equip.LastModifiedBy = currentUser.Name;
        }

        public async Task CreateNewMainTask(int EquipId,CreateMainTaskDTO Maintask)
        {
            var mainTask = mapper.Map<MainTask>(Maintask);

            var MainHis = await MaintenanceHistoryService.GetMaintenanceHistoryByEquipId(EquipId);

            mainTask.EquipmentId = EquipId;

            mainTask.CreatedBy = currentUser.Name;

            AuditModify(mainTask);

            foreach (var mainHis in MainHis)
            {
                if(mainHis.StartDate == DateOnly.FromDateTime(DateTime.Now))
                {
                    await MaintenanceHistoryService.AddNewTasktoHistory(mainHis.HistoryId,mainTask);
                    return;
                }
            }

            await MainTaskRepository.CreateNewTask(mainTask);

            var equip = await EquipmentReposity.GetEquipmentById(EquipId);

            CreateMaintenanceHistoryDTO mainhisDTO = new CreateMaintenanceHistoryDTO();

            mainhisDTO.EquipmentId = EquipId;
            mainhisDTO.EquipmentName = equip.Name;
            mainhisDTO.EquipmentType = equip.Type;
            mainhisDTO.StartDate = DateOnly.FromDateTime(DateTime.Now);

            await MaintenanceHistoryService.CreateMaintenanceHistoryFromTask(mainhisDTO, mainTask,equip);

            equip.AddMainTask(mainTask);


        }
    }
}
