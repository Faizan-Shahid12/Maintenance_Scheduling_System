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

        public ScheduleTaskService(IMapper mapper, ICurrentUser currentUser, IScheduleTaskRepo scheduleTaskRepository)
        {
            this.mapper = mapper;
            this.currentUser = currentUser;
            ScheduleTaskRepository = scheduleTaskRepository;
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
                AuditModify(scheduleTask);
            }
        }
        public async Task DeleteScheduleTasks(List<ScheduleTask> scheduleTasks)
        {
            foreach (ScheduleTask scheduleTask in scheduleTasks.ToList())
            {
                scheduleTask.IsDeleted = true;
                AuditModify(scheduleTask);
            }
        }
        public async Task CreateAuditScheduleTask(ScheduleTask scheduleTasks)
        {

                scheduleTasks.CreatedBy = currentUser.Name;
                AuditModify(scheduleTasks);
            
        }
        public async Task DeleteScheduleTask(ScheduleTask scheduleTasks)
        {
            
                scheduleTasks.IsDeleted = true;
                AuditModify(scheduleTasks);
                
                await ScheduleTaskRepository.DeleteScheduleTask();
            
        }

        public async Task ChangeDueDate(List<ScheduleTask> scheduleTasks,DateOnly startDate)
        {
            foreach(ScheduleTask scheduleTask in scheduleTasks.ToList())
            {
                scheduleTask.DueDate = startDate;
            }
            await ScheduleTaskRepository.UpdateScheduleTask();
        }

    }
}
