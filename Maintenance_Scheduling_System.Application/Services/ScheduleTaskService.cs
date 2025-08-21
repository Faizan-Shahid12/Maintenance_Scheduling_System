using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
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
    public class ScheduleTaskService : IScheduleTaskService
    {
        private IMapper mapper { get; set; }
        private ICurrentUser currentUser { get; set; }
        private IScheduleTaskRepo ScheduleTaskRepository { get; set; }
        private IAppUserService appUserService { get; set; }

        public ScheduleTaskService(IMapper mapper, ICurrentUser currentUser, IScheduleTaskRepo scheduleTaskRepository, IAppUserService appUser)
        {
            this.mapper = mapper;
            this.currentUser = currentUser;
            ScheduleTaskRepository = scheduleTaskRepository;
            appUserService = appUser;
        }
        
        public async Task AuditModify(ScheduleTask schedule)
        {
            schedule.LastModifiedAt = DateTime.Now;
            schedule.LastModifiedBy = currentUser.Name;
        }

        public async Task CreateAuditScheduleTasks(List<ScheduleTask> scheduleTasks)
        {
            foreach (ScheduleTask scheduleTask in scheduleTasks.ToList())
            {
                scheduleTask.CreatedBy = currentUser.Name;
                await AuditModify(scheduleTask);
            }
        }
        public async Task DeleteScheduleTasks(List<ScheduleTask> scheduleTasks)
        {
            foreach (ScheduleTask scheduleTask in scheduleTasks.ToList())
            {
                scheduleTask.IsDeleted = true;
                await AuditModify(scheduleTask);
            }
        }
        public async Task CreateAuditScheduleTask(ScheduleTask scheduleTasks)
        {

                scheduleTasks.CreatedBy = currentUser.Name;
                await AuditModify(scheduleTasks);
            
        }
        public async Task DeleteScheduleTask(ScheduleTask scheduleTasks)
        {
            
                scheduleTasks.IsDeleted = true;
                await AuditModify(scheduleTasks);
                
                await ScheduleTaskRepository.DeleteScheduleTask();
            
        }

        public async Task<ScheduleTaskDTO> EditScheduleTask(ScheduleTask Task, ScheduleTaskDTO  STDTO)
        {
            
            Task.DueDate = STDTO.DueDate;
            Task.TaskName = STDTO.TaskName;
            Task.Priority = STDTO.Priority;
            Task.Interval = STDTO.Interval;

            await ScheduleTaskRepository.UpdateScheduleTask();

            return mapper.Map<ScheduleTaskDTO>(Task);
        }

        public async Task<ScheduleTaskDTO> AssignTechnician(ScheduleTask Task, string? techId)
        {
            if (techId != "")
            {
                Task.TechnicianId = techId;
            }
            await AuditModify(Task);
            await ScheduleTaskRepository.UpdateScheduleTask();
            return mapper.Map<ScheduleTaskDTO>(Task);
        }

        public async Task ChangeDueDate(DateOnly ScheduleDate,List<ScheduleTask> scheduleTasks)
        {
            foreach(ScheduleTask scheduleTask in scheduleTasks.ToList())
            {
                scheduleTask.DueDate = ScheduleDate.AddDays((int)scheduleTask.Interval.TotalDays);
            }
            await ScheduleTaskRepository.UpdateScheduleTask();
        }

        public async Task ChangeAssignedInDTO (ScheduleTaskDTO task,ScheduleTask scheduleTask)
        {
            if (scheduleTask?.TechnicianId != null)
            {
                var tech = await appUserService.GetTechnicianById(scheduleTask.TechnicianId);

                if (tech != null)
                {
                    task.AssignedTo = tech.FullName;
                    task.TechEmail = tech.Email;
                }
                else
                {
                    task.AssignedTo = "Technician Not Found";
                }
            }
            else
            {
                task.AssignedTo = "N/A";
            }


        }

    }
}
