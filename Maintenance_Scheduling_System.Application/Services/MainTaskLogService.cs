using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
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
    public class MainTaskLogService : IMainTaskLogService
    {
        public IMapper mapper { get; set; }
        public IMainTaskRepo MainTaskRepository { get; set; }
        public ITaskLogRepo TaskLogRepository { get; set; }

        public ITaskLogAttachmentService TaskLogAttachmentService { get; set; }

        public ICurrentUser currentUser { get; set; }

        public MainTaskLogService(IMapper map,IMainTaskRepo Mainrepo, ITaskLogRepo logrepo, ICurrentUser user, ITaskLogAttachmentService taskLogAttachmentService)
        {
            mapper = map;
            MainTaskRepository = Mainrepo;
            TaskLogRepository = logrepo;
            currentUser = user;
            TaskLogAttachmentService = taskLogAttachmentService;
        }
        private void AuditModify(TaskLogs equip)
        {
            equip.LastModifiedAt = DateTime.Now;
            equip.LastModifiedBy = currentUser.Name;
        }
        public async Task CreateTaskLog(CreateTaskLogDTO taskdto)
        {
            var Mtask = await MainTaskRepository.GetTaskById(taskdto.TaskId);
            var tasklog = mapper.Map<TaskLogs>(taskdto);

            tasklog.TaskId = taskdto.TaskId;
            tasklog.CreatedBy = currentUser.Name;

            AuditModify(tasklog);

            await TaskLogRepository.CreateNewTaskLogs(tasklog);
        }

        public async Task<List<TaskLogDTO>> GetAllTaskLog(int TaskId)
        {
            var logs = await TaskLogRepository.GetAllTaskLogsByTaskId(TaskId);
            var logdto = mapper.Map<List<TaskLogDTO>>(logs);
            return logdto;
        }

        public async Task UpdateTaskLog(TaskLogDTO tasklog)
        {
            var log = await TaskLogRepository.GetTaskLogByLogId(tasklog.LogId);
            log.Note = tasklog.Note;
            log.LCreatedAt = tasklog.CreatedAt;
            log.LCreatedBy = tasklog.CreatedBy;
            AuditModify(log);

            await TaskLogRepository.UpdateTaskLogs(log);
        }

        public async Task DeleteTaskLog(int logId)
        {
            var log = await TaskLogRepository.GetTaskLogByLogId(logId);
            log.IsDeleted = true;
            AuditModify(log);

            await TaskLogRepository.DeleteTaskLogs(log);

            await TaskLogAttachmentService.DeleteAttachmentByLog(logId);
        }
    }
}
