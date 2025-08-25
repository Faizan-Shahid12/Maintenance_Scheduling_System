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
        private IEquipmentService EquipmentService { get; set; }

        // IN PROGRESS
        public MaintenanceScheduleService(IMaintenanceScheduleRepo maintenanceScheduleRepository, IMapper mapper, ICurrentUser currentUser,IScheduleTaskService scheduleTaskService,IMainTaskService main,IEquipmentService equip)
        {
            MaintenanceScheduleRepository = maintenanceScheduleRepository;
            this.mapper = mapper;
            this.currentUser = currentUser;
            ScheduleTaskService = scheduleTaskService;
            MainTaskService = main;
            EquipmentService = equip;
        }
        private void ModifyAudit(MaintenanceSchedule maintenanceSchedule)
        {
            maintenanceSchedule.LastModifiedAt = DateTime.Now;
            maintenanceSchedule.LastModifiedBy = currentUser.Name;
        }
        public async Task<DisplayMaintenanceScheduleDTO> CreateMaintenanceSchedule(CreateMaintenanceScheduleDTO msdto)
        {
            var MaintenanceSchedule = mapper.Map<MaintenanceSchedule>(msdto);

            var ScheduleTask = mapper.Map<List<ScheduleTask>>(msdto.ScheduleTasks);

            await ScheduleTaskService.CreateAuditScheduleTasks(ScheduleTask);

            MaintenanceSchedule.CreatedBy = currentUser.Name;
            MaintenanceSchedule.ScheduleTasks = ScheduleTask;
            MaintenanceSchedule.EquipmentId = msdto.EquipId;

            ModifyAudit(MaintenanceSchedule);
            
            await MaintenanceScheduleRepository.CreateNewMaintenanceSchedule(MaintenanceSchedule);

            var DTO = mapper.Map<DisplayMaintenanceScheduleDTO>(MaintenanceSchedule);

            await ChangeInAssigned(MaintenanceSchedule, DTO);

            return DTO;
        }

        public async Task<DisplayMaintenanceScheduleDTO> UpdateMaintenanceSchedule(MaintenanceScheduleDTO msdto)
        {
            var MaintenanceSchedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(msdto.ScheduleId);

            if (MaintenanceSchedule == null)
                throw new Exception("Schedule not found");

            if(MaintenanceSchedule.StartDate != msdto.StartDate)
            {
                MaintenanceSchedule.StartDate = msdto.StartDate;

                await ScheduleTaskService.ChangeDueDate(msdto.StartDate, MaintenanceSchedule.ScheduleTasks);
            }

            MaintenanceSchedule.ScheduleName = msdto.ScheduleName;
            MaintenanceSchedule.ScheduleType = msdto.ScheduleType;
            MaintenanceSchedule.EndDate = msdto.EndDate;
            MaintenanceSchedule.IsActive = msdto.IsActive;
            MaintenanceSchedule.Interval = msdto.Interval;

            ModifyAudit(MaintenanceSchedule);

            await MaintenanceScheduleRepository.UpdateMaintenanceSchedule();

            var DTO = mapper.Map<DisplayMaintenanceScheduleDTO>(MaintenanceSchedule);

            await ChangeInAssigned(MaintenanceSchedule, DTO);

            return DTO;
        }

        public async Task<DisplayMaintenanceScheduleDTO> DeleteMaintenanceSchedule(int MSId)
        {
            var MaintenanceSchedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(MSId);

            if (MaintenanceSchedule == null)
                throw new Exception("Schedule not found");

            ModifyAudit(MaintenanceSchedule);

            await ScheduleTaskService.DeleteScheduleTasks(MaintenanceSchedule.ScheduleTasks);

            MaintenanceSchedule.IsDeleted = true;

            await MaintenanceScheduleRepository.DeleteMaintenanceSchedule();

            return mapper.Map<DisplayMaintenanceScheduleDTO>(MaintenanceSchedule);

        }

        public async Task<DisplayMaintenanceScheduleDTO> AddNewTasktoSchedule(int ScheduleId,CreateScheduleTaskDTO stdto)
        {
            var Schedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(ScheduleId);

            var equipment = await EquipmentService.GetEquipmentById(Schedule.EquipmentId);

            if (Schedule == null)
                throw new Exception("Schedule not found");

            var ScheduleTask = mapper.Map<ScheduleTask>(stdto);

            ScheduleTask.ScheduleId = ScheduleId;
            ScheduleTask.EquipmentName = equipment.Name;
            
            await ScheduleTaskService.CreateAuditScheduleTask(ScheduleTask);

            Schedule.ScheduleTasks.Add(ScheduleTask);

            ModifyAudit(Schedule);

            await MaintenanceScheduleRepository.UpdateMaintenanceSchedule();

            var DTO = mapper.Map<DisplayMaintenanceScheduleDTO>(Schedule);

            await ChangeInAssigned(Schedule, DTO);

            return DTO;

        }
        public async Task<DisplayMaintenanceScheduleDTO> DeleteTaskFromSchedule(int ScheduleId, int ScheduleTaskId)
        {
            var Schedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(ScheduleId);

            if (Schedule == null)
                throw new Exception("Schedule not found");

            var ScheduleTask = Schedule.ScheduleTasks.FirstOrDefault(x => x.ScheduleTaskId == ScheduleTaskId);

            if (ScheduleTask == null)
                throw new Exception("Task not found");

            ModifyAudit(Schedule);

            await ScheduleTaskService.DeleteScheduleTask(ScheduleTask);

            Schedule.ScheduleTasks.Remove(ScheduleTask);

            var DTO = mapper.Map<DisplayMaintenanceScheduleDTO>(Schedule);

            await ChangeInAssigned(Schedule, DTO);

            return DTO;

        }

        public async Task<DisplayMaintenanceScheduleDTO> UnActivateSchedule(int ScheduleId)
        {
            var Schedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(ScheduleId);
            Schedule.IsActive = false;
            ModifyAudit(Schedule);
            await MaintenanceScheduleRepository.UpdateMaintenanceSchedule();

            var DTO = mapper.Map<DisplayMaintenanceScheduleDTO>(Schedule);

            await ChangeInAssigned(Schedule, DTO);

            return DTO;

        }
        public async Task<DisplayMaintenanceScheduleDTO> ActivateSchedule(int ScheduleId)
        {
            var Schedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(ScheduleId);
            Schedule.IsActive = true;
            ModifyAudit(Schedule);
            await MaintenanceScheduleRepository.UpdateMaintenanceSchedule();
            var DTO = mapper.Map<DisplayMaintenanceScheduleDTO>(Schedule);

            await ChangeInAssigned(Schedule, DTO);

            return DTO;

        }

        public async Task<List<DisplayMaintenanceScheduleDTO>> GetMaintenanceScheduleByEquipmentId(int EquipmentId)
        {
            var schedule = await MaintenanceScheduleRepository.GetAllMaintenanceScheduleByEquipId(EquipmentId);

            var ScheduleDTO = mapper.Map<List<DisplayMaintenanceScheduleDTO>>(schedule);

            for (int i = 0; i < schedule.Count; i++)
            {
                var schedule1 = schedule[i];
                var schedule2 = ScheduleDTO[i];

                var ScheduleTaskDTO1 = schedule1.ScheduleTasks
                    .Select(task =>
                    {
                        var dto = mapper.Map<ScheduleTaskDTO>(task);

                        ScheduleTaskService.ChangeAssignedInDTO(dto, task);

                        return dto;
                    })
                    .ToList();
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

                var scheduleTaskDTO1 = new List<ScheduleTaskDTO>();

                foreach (var task in schedule1.ScheduleTasks)
                {
                    var dto = mapper.Map<ScheduleTaskDTO>(task);
                    await ScheduleTaskService.ChangeAssignedInDTO(dto, task);
                    scheduleTaskDTO1.Add(dto);
                }

                ScheduleDTO[i].ScheduleTasks = scheduleTaskDTO1;
            }

            return ScheduleDTO;
        }

        public async Task<DisplayMaintenanceScheduleDTO> EditScheduleTask(int ScheduleId, ScheduleTaskDTO scheduleTaskDTO)
        {

            var Schedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(ScheduleId);

            if (Schedule == null)
                throw new Exception("Schedule not found");

            var ScheduleTask = Schedule.ScheduleTasks.FirstOrDefault(x => x.ScheduleTaskId == scheduleTaskDTO.ScheduleTaskId);

            if (ScheduleTask == null)
                throw new Exception("Task not found");

            ModifyAudit(Schedule);

            await ScheduleTaskService.EditScheduleTask(ScheduleTask,scheduleTaskDTO);

            var DTO = mapper.Map<DisplayMaintenanceScheduleDTO>(Schedule);

            await ChangeInAssigned(Schedule, DTO);

            return DTO;
        }

        public async Task<DisplayMaintenanceScheduleDTO> AssignTechnicianToScheduleTask(int ScheduleId,int ScheduleTaskId,string? TechId)
        {
            var Schedule = await MaintenanceScheduleRepository.GetMaintenanceScheduleById(ScheduleId);

            if (Schedule == null)
                throw new Exception("Schedule not found");

            var ScheduleTask = Schedule.ScheduleTasks.FirstOrDefault(x => x.ScheduleTaskId == ScheduleTaskId);

            if (ScheduleTask == null)
                throw new Exception("Task not found");

            ModifyAudit(Schedule);

            await ScheduleTaskService.AssignTechnician(ScheduleTask, TechId);

            var DTO =  mapper.Map<DisplayMaintenanceScheduleDTO>(Schedule);

            await ChangeInAssigned(Schedule, DTO);

            return DTO;
        }

        private async Task<DisplayMaintenanceScheduleDTO> ChangeInAssigned(MaintenanceSchedule schedule, DisplayMaintenanceScheduleDTO scheduleDTO)
        {
            for (int i = 0; i < scheduleDTO.ScheduleTasks.Count; i++)
            {
                await ScheduleTaskService.ChangeAssignedInDTO(scheduleDTO.ScheduleTasks[i], schedule.ScheduleTasks[i]);
            }

            return scheduleDTO;
        }

        public async Task<List<DisplayMaintenanceScheduleDTO>> GetAllMaintenanceScheduleByStartDate()
        {
            var schedule = await MaintenanceScheduleRepository.GetAllMaintenanceSchedule();

            var ScheduleDTO = mapper.Map<List<DisplayMaintenanceScheduleDTO>>(schedule);

            for (int i = 0; i < schedule.Count; i++)
            {
                var schedule1 = schedule[i];
                var schedule2 = ScheduleDTO[i];

                var ScheduleTaskDTO1 = schedule1.ScheduleTasks
                    .Select(task =>
                    {
                        var dto = mapper.Map<ScheduleTaskDTO>(task);

                        ScheduleTaskService.ChangeAssignedInDTO(dto, task);

                        return dto;
                    })
                    .ToList();
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

                if(maintenanceSchedule.EndDate != null && (maintenanceSchedule.EndDate <= DateOnly.FromDateTime(DateTime.Now) || maintenanceSchedule.EndDate < maintenanceSchedule.StartDate))
                {
                    maintenanceSchedule.IsActive = false;

                    await MaintenanceScheduleRepository.UpdateMaintenanceSchedule();
                }
               
            }
        }

        public async Task AutomaticallyGenerate()
        {
            var Schedules = await MaintenanceScheduleRepository.GetAllMaintenanceSchedule();
            var Equipments = await EquipmentService.GetAllEquipments();

            foreach (MaintenanceSchedule maintenanceSchedule in Schedules)
            {
                if(maintenanceSchedule == null) continue;

                if(Equipments.FirstOrDefault(t => t.EquipmentId == maintenanceSchedule.EquipmentId) == null || Equipments.FirstOrDefault(t => t.EquipmentId == maintenanceSchedule.EquipmentId).isArchived)  continue;

                if(maintenanceSchedule.StartDate <= DateOnly.FromDateTime(DateTime.Now) && maintenanceSchedule.IsActive)
                {
                    maintenanceSchedule.LastGeneratedDate = maintenanceSchedule.StartDate;
                    maintenanceSchedule.StartDate = maintenanceSchedule.StartDate.AddDays((int) maintenanceSchedule.Interval.TotalDays);
                    await MainTaskService.CreateNewMainTaskByScheduleTask(maintenanceSchedule.EquipmentId,maintenanceSchedule.ScheduleTasks);
                    await ScheduleTaskService.ChangeDueDate(maintenanceSchedule.StartDate,maintenanceSchedule.ScheduleTasks);
                }
            }
        }
    }
}
