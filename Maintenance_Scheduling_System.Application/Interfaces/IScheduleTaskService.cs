using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using Maintenance_Scheduling_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Interfaces
{
    public interface IScheduleTaskService
    {
        Task AuditModify(ScheduleTask scheduleTask);
        Task CreateAuditScheduleTasks(List<ScheduleTask> scheduleTasks);
        Task CreateAuditScheduleTask(ScheduleTask scheduleTask);
        Task DeleteScheduleTasks(List<ScheduleTask> scheduleTasks);
        Task DeleteScheduleTask(ScheduleTask scheduleTask);
        Task<ScheduleTaskDTO> EditScheduleTask(ScheduleTask Task,ScheduleTaskDTO STDTO);
        Task<ScheduleTaskDTO> AssignTechnician(ScheduleTask Task, string techId);

        Task ChangeDueDate(DateOnly ScheduleDate, List<ScheduleTask> scheduleTasks);
        Task ChangeAssignedInDTO(ScheduleTaskDTO task, ScheduleTask scheduleTask);


    }
}
