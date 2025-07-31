using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Services
{
    public class MaintenanceScheduleService : IMaintenanceScheduleService
    {
        private IMaintenanceScheduleRepo MaintenanceScheduleRepository { get; set; }
        private IMapper mapper { get; set; }
        private ICurrentUser currentUser { get; set; }
        private IScheduleTaskService ScheduleTaskService { get; set; }
        private IMainTaskService MainTaskService { get; set; }
        public MaintenanceScheduleService(IMaintenanceScheduleRepo maintenanceScheduleRepository, IMapper mapper, ICurrentUser currentUser,IScheduleTaskService scheduleTaskService,IMainTaskService main)
        {
            MaintenanceScheduleRepository = maintenanceScheduleRepository;
            this.mapper = mapper;
            this.currentUser = currentUser;
            ScheduleTaskService = scheduleTaskService;
            MainTaskService = main;
        }
        private void ModifyAudit(MaintenanceSchedule maintenanceSchedule)
        {
            maintenanceSchedule.LastModifiedAt = DateTime.Now;
            maintenanceSchedule.LastModifiedBy = currentUser.Name;
        }
        public async Task CreateMaintenanceSchedule(CreateMaintenanceScheduleDTO msdto)
        {
            var MaintenanceSchedule = mapper.Map<MaintenanceSchedule>(msdto);
            var ScheduleTask = mapper.Map<List<ScheduleTask>>(msdto.ScheduleTasks);

            await ScheduleTaskService.CreateAuditScheduleTasks(ScheduleTask);

            MaintenanceSchedule.CreatedBy = currentUser.Name;
            MaintenanceSchedule.ScheduleTasks = ScheduleTask;
            MaintenanceSchedule.EquipmentId = msdto.EquipId;
            ModifyAudit(MaintenanceSchedule);
            
            await MaintenanceScheduleRepository.CreateNewMaintenanceSchedule(MaintenanceSchedule);

        }

        public async Task UpdateMaintenanceSchedule(MaintenanceScheduleDTO msdto)
        {
            var MaintenanceSchedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(msdto.ScheduleId);

            if (MaintenanceSchedule == null)
                throw new Exception("Schedule not found");

            MaintenanceSchedule.ScheduleName = msdto.ScheduleName;
            MaintenanceSchedule.ScheduleType = msdto.ScheduleType;
            MaintenanceSchedule.StartDate = msdto.StartDate;
            MaintenanceSchedule.EndDate = msdto.EndDate;
            MaintenanceSchedule.IsActive = msdto.IsActive;
            MaintenanceSchedule.Interval = msdto.Interval;
            ModifyAudit(MaintenanceSchedule);

            await MaintenanceScheduleRepository.UpdateMaintenanceSchedule();
        }

        public async Task DeleteMaintenanceSchedule(int MSId)
        {
            var MaintenanceSchedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(MSId);

            if (MaintenanceSchedule == null)
                throw new Exception("Schedule not found");

            ModifyAudit(MaintenanceSchedule);

            await ScheduleTaskService.DeleteScheduleTasks(MaintenanceSchedule.ScheduleTasks);

            MaintenanceSchedule.IsDeleted = true;

            await MaintenanceScheduleRepository.DeleteMaintenanceSchedule();
        }

        public async Task AddNewTasktoSchedule(int ScheduleId,CreateScheduleTaskDTO stdto)
        {
            var Schedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(ScheduleId);

            if (Schedule == null)
                throw new Exception("Schedule not found");

            var ScheduleTask = mapper.Map<ScheduleTask>(stdto);

            ScheduleTask.ScheduleId = ScheduleId;
            
            await ScheduleTaskService.CreateAuditScheduleTask(ScheduleTask);

            Schedule.ScheduleTasks.Add(ScheduleTask);

            ModifyAudit(Schedule);

            await MaintenanceScheduleRepository.UpdateMaintenanceSchedule();
        }
        public async Task DeleteTaskFromSchedule(int ScheduleId, int ScheduleTaskId)
        {
            var Schedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(ScheduleId);

            if (Schedule == null)
                throw new Exception("Schedule not found");

            var ScheduleTask = Schedule.ScheduleTasks.FirstOrDefault(x => x.ScheduleTaskId == ScheduleTaskId);

            if (ScheduleTask == null)
                throw new Exception("Task not found");

            ModifyAudit(Schedule);

            await ScheduleTaskService.DeleteScheduleTask(ScheduleTask);
        }

        public async Task UnActivateSchedule(int ScheduleId)
        {
            var Schedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(ScheduleId);
            Schedule.IsActive = false;
            ModifyAudit(Schedule);
            await MaintenanceScheduleRepository.UpdateMaintenanceSchedule();
        }
        public async Task ActivateSchedule(int ScheduleId)
        {
            var Schedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(ScheduleId);
            Schedule.IsActive = true;
            ModifyAudit(Schedule);
            await MaintenanceScheduleRepository.UpdateMaintenanceSchedule();
        }

        public async Task<List<DisplayMaintenanceScheduleDTO>> GetMaintenanceScheduleByEquipmentId(int EquipmentId)
        {
            var schedule = await MaintenanceScheduleRepository.GetAllMaintenanceScheduleByEquipId(EquipmentId);

            var ScheduleDTO = mapper.Map<List<DisplayMaintenanceScheduleDTO>>(schedule);

            for (int i = 0; i < schedule.Count; i++)
            {
                var schedule1 = schedule[i];
                var schedule2 = ScheduleDTO[i];
            
                var ScheduleTaskDTO1 = mapper.Map<List<ScheduleTaskDTO>>(schedule1.ScheduleTasks);
                ScheduleDTO[i].ScheduleTasks = ScheduleTaskDTO1;
            }

            return ScheduleDTO;
        }

        public async Task<List<DisplayMaintenanceScheduleDTO>> GetAllMaintenanceSchedule()
        {
            var schedule = await MaintenanceScheduleRepository.GetAllMaintenanceSchedule();

            var ScheduleDTO = mapper.Map<List<DisplayMaintenanceScheduleDTO>>(schedule);

            for (int i = 0; i < schedule.Count; i++)
            {
                var schedule1 = schedule[i];
                var schedule2 = ScheduleDTO[i];

                var ScheduleTaskDTO1 = mapper.Map<List<ScheduleTaskDTO>>(schedule1.ScheduleTasks);
                ScheduleDTO[i].ScheduleTasks = ScheduleTaskDTO1;
            }

            return ScheduleDTO;
        }
        public async Task<List<DisplayMaintenanceScheduleDTO>> GetAllMaintenanceScheduleByStartDate()
        {
            var schedule = await MaintenanceScheduleRepository.GetAllMaintenanceSchedule();

            var ScheduleDTO = mapper.Map<List<DisplayMaintenanceScheduleDTO>>(schedule);

            for (int i = 0; i < schedule.Count; i++)
            {
                var schedule1 = schedule[i];
                var schedule2 = ScheduleDTO[i];

                var ScheduleTaskDTO1 = mapper.Map<List<ScheduleTaskDTO>>(schedule1.ScheduleTasks);
                ScheduleDTO[i].ScheduleTasks = ScheduleTaskDTO1;
            }

            var sortedList = ScheduleDTO.OrderBy(x => x.StartDate).ToList();

            return sortedList;
        }

        public async Task AutomaticallyUnactivate()
        {
            var Schedules = await MaintenanceScheduleRepository.GetAllMaintenanceSchedule();

            foreach (MaintenanceSchedule maintenanceSchedule in Schedules)
            {
                if (maintenanceSchedule == null) continue;

                if(maintenanceSchedule.EndDate != null && maintenanceSchedule.EndDate <= DateOnly.FromDateTime(DateTime.Now))
                {
                    maintenanceSchedule.IsActive = false;
                }
               
            }
        }

        public async Task AutomaticallyGenerate()
        {
            var Schedules = await MaintenanceScheduleRepository.GetAllMaintenanceSchedule();

            foreach (MaintenanceSchedule maintenanceSchedule in Schedules)
            {
                if(maintenanceSchedule == null) continue;

                if(maintenanceSchedule.StartDate <= DateOnly.FromDateTime(DateTime.Now) && maintenanceSchedule.IsActive)
                {
                    maintenanceSchedule.LastGeneratedDate = maintenanceSchedule.StartDate;
                    maintenanceSchedule.StartDate = maintenanceSchedule.StartDate.AddDays((int) maintenanceSchedule.Interval.TotalDays);
                    await MainTaskService.CreateNewMainTaskByScheduleTask(maintenanceSchedule.EquipmentId,maintenanceSchedule.ScheduleTasks);
                    await ScheduleTaskService.ChangeDueDate(maintenanceSchedule.ScheduleTasks,maintenanceSchedule.StartDate);
                }
            }
        }
    }
}
