using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.Enums;
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
            equip.LastModifiedAt = DateTime.Now;
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

        public async Task CreateNewMainTaskByScheduleTask(int EquipId, List<ScheduleTask> scheduleTasks)
        {
            return;
        }

        public async Task<List<MainTaskDTO>> GetAllMainTask()
        {
            var task = await MainTaskRepository.GetAllTask();
            var taskdto = mapper.Map<List<MainTaskDTO>>(task);

            return taskdto;
        }
        public async Task<List<MainTaskDTO>> GetAllOverDueTask()
        {
            var task = await MainTaskRepository.GetTaskByStatus(StatusEnum.OverDue);
            var taskdto = mapper.Map<List<MainTaskDTO>>(task);

            return taskdto;
        }
        public async Task<List<MainTaskDTO>> GetAllCompletedTask()
        {
            var task = await MainTaskRepository.GetTaskByStatus(StatusEnum.Completed);
            var taskdto = mapper.Map<List<MainTaskDTO>>(task);

            return taskdto;
        }

        public async Task<List<MainTaskDTO>> GetMainTaskByEquipmentId(int EquipId)
        {
            var task = await MainTaskRepository.GetAllTaskByEquipId(EquipId);
            var taskDto = mapper.Map<List<MainTaskDTO>>(task);

            return taskDto;
        }
        public async Task DeleteTask(int TaskId)
        {
            var task = await MainTaskRepository.GetTaskById(TaskId);
            task.IsDeleted = true;
            AuditModify(task);
            await MainTaskRepository.DeleteTask();
        }
        public async Task UpdateTask(MainTaskDTO taskdto)
        {
            var task = await MainTaskRepository.GetTaskById(taskdto.TaskId);
            task.TaskName = taskdto.TaskName;
            task.EquipmentName = taskdto.EquipmentName;
            task.DueDate = taskdto.DueDate;
            task.Priority = taskdto.Priority;
            task.Status = taskdto.Status;

            AuditModify(task);
            await MainTaskRepository.UpdateTask();
        }

        public async Task UpdatePriority(int TaskId,PriorityEnum priority)
        {
            var task = await MainTaskRepository.GetTaskById(TaskId);
            task.Priority = priority;
            AuditModify(task);
            await MainTaskRepository.UpdateTask();
        }

        public async Task ChangeTaskStatus(int TaskId, StatusEnum status)
        {
            var task = await MainTaskRepository.GetTaskById(TaskId);
            task.Status = status;
            AuditModify(task);
            await MainTaskRepository.UpdateTask();
        }

        public async Task OverDueTask()
        {
            var task = await MainTaskRepository.GetAllTask();

            if (task == null) return;

            foreach (MainTask mainTask in task.ToList())
            {
                if(mainTask.DueDate < DateOnly.FromDateTime(DateTime.Now) && mainTask.Status != StatusEnum.Completed)
                {
                    await ChangeTaskStatus(mainTask.TaskId, StatusEnum.OverDue);
                }
            }

        }
        public async Task AssignTechnician(int TaskId, string TechId)
        {
            var task = await MainTaskRepository.GetTaskById(TaskId);
            task.TechnicianId = TechId;
            AuditModify(task);
            await MainTaskRepository.UpdateTask();
        }

    }
}
