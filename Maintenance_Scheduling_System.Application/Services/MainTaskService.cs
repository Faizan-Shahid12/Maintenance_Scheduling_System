using AutoMapper;
using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
using Maintenance_Scheduling_System.Application.HubInterfaces;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Maintenance_Scheduling_System.Domain.Enums;
using Maintenance_Scheduling_System.Domain.IRepo;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_Scheduling_System.Application.Services
{
    //DONE
    public class MainTaskService : IMainTaskService
    {
        public IMapper mapper {  get; set; }
        public IEquipmentRepo EquipmentReposity { get; set; }
        public IMainTaskRepo MainTaskRepository { get; set; }
        public IMaintenanceHistoryService MaintenanceHistoryService { get; set; }
        public ICurrentUser currentUser { get; set; }
        public ITaskHub TaskHub {  get; set; }

        public MainTaskService(IMapper mapper1,IEquipmentRepo Erepo, IMaintenanceHistoryService MHS,ICurrentUser cu,IMainTaskRepo task,ITaskHub hub)
        {
            mapper = mapper1;
            EquipmentReposity = Erepo;
            MaintenanceHistoryService = MHS;
            currentUser = cu;
            MainTaskRepository = task;
            TaskHub = hub;
        }

        private void AuditModify(MainTask equip)
        {
            equip.LastModifiedAt = DateTime.Now;
            equip.LastModifiedBy = currentUser.Name;
        }

        public async Task<MainTaskDTO> CreateNewMainTask(int EquipId,CreateMainTaskDTO Maintask)
        {
            var mainTask = mapper.Map<MainTask>(Maintask);

            var MainHis = await MaintenanceHistoryService.GetMaintenanceHistoryByEquipId(EquipId);

            var equip = await EquipmentReposity.GetEquipmentById(EquipId);

            mainTask.EquipmentId = EquipId;

            mainTask.EquipmentName = equip.Name;

            mainTask.CreatedBy = currentUser.Name;

            AuditModify(mainTask);

            bool check = false;

            foreach (var mainHis in MainHis)
            {
                if(mainHis.StartDate == DateOnly.FromDateTime(DateTime.Now))
                {
                    await MaintenanceHistoryService.AddNewTasktoHistory(mainHis.HistoryId,mainTask);
                    check = true;
                }
            }

            var task = mainTask;

            if (check == false)
            {
                task = await MainTaskRepository.CreateNewTask(mainTask);
                CreateMaintenanceHistoryDTO mainhisDTO = new CreateMaintenanceHistoryDTO();

                mainhisDTO.EquipmentId = EquipId;
                mainhisDTO.EquipmentName = equip.Name;
                mainhisDTO.EquipmentType = equip.Type;
                mainhisDTO.StartDate = DateOnly.FromDateTime(DateTime.Now);

                await MaintenanceHistoryService.CreateMaintenanceHistoryFromTask(mainhisDTO, mainTask, equip);

                equip.AddMainTask(mainTask);
            }

            var task1 = mapper.Map<MainTaskDTO>(task);

            await TaskHub.SendTaskToClient(task1, task.TechnicianId);

            return task1;

        }

        public async Task<List<MainTaskDTO>> CreateNewMainTaskByScheduleTask(int EquipId, List<ScheduleTask> scheduleTasks)
        {
            List<MainTaskDTO> taskdto = new();

            foreach (var scheduleTask in scheduleTasks)
            {
                var mainTask = new MainTask{
                    EquipmentName= scheduleTask.EquipmentName,
                    TaskName= scheduleTask.TaskName,
                    DueDate= scheduleTask.DueDate,
                    Priority = scheduleTask.Priority
                };

                if (scheduleTask.TechnicianId != null)
                {
                    mainTask.TechnicianId = scheduleTask.TechnicianId;
                }

                var MainHis = await MaintenanceHistoryService.GetMaintenanceHistoryByEquipId(EquipId);

                mainTask.EquipmentId = EquipId;

                mainTask.CreatedBy = currentUser.Name;

                AuditModify(mainTask);

                bool check = false;

                foreach (var mainHis in MainHis)
                {
                    if (mainHis.StartDate == DateOnly.FromDateTime(DateTime.Now))
                    {
                        await MaintenanceHistoryService.AddNewTasktoHistory(mainHis.HistoryId, mainTask);
                        check = true;
                    }
                }
                var task = mainTask;

                if (check == false)
                {
                    task = await MainTaskRepository.CreateNewTask(mainTask);

                    var equip = await EquipmentReposity.GetEquipmentById(EquipId);

                    CreateMaintenanceHistoryDTO mainhisDTO = new CreateMaintenanceHistoryDTO();

                    mainhisDTO.EquipmentId = EquipId;
                    mainhisDTO.EquipmentName = equip.Name;
                    mainhisDTO.EquipmentType = equip.Type;
                    mainhisDTO.StartDate = DateOnly.FromDateTime(DateTime.Now);

                    await MaintenanceHistoryService.CreateMaintenanceHistoryFromTask(mainhisDTO, mainTask, equip);

                    equip.AddMainTask(mainTask);
                }
                
                if(mainTask.TechnicianId != null)
                  await MainTaskRepository.LoadTechnician(mainTask);


                var task1 = mapper.Map<MainTaskDTO>(task);

                taskdto.Add(task1);

                await TaskHub.SendTaskToClient(task1, task.TechnicianId);

            }

            return taskdto;
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

        public async Task<List<MainTaskDTO>> GetMainTaskByHistoryId(int HistoryId)
        {
            var task = await MainTaskRepository.GetMainTaskByHistoryId(HistoryId);

            var taskDto = mapper.Map<List<MainTaskDTO>>(task);

            return taskDto;
        }
        public async Task<MainTaskDTO> DeleteTask(int TaskId)
        {
            var task = await MainTaskRepository.GetTaskById(TaskId);

            task.IsDeleted = true;

            AuditModify(task);

            await MainTaskRepository.DeleteTask();

            return mapper.Map<MainTaskDTO>(task);
        }
        public async Task<MainTaskDTO> UpdateTask(MainTaskDTO taskdto)
        {
            var task = await MainTaskRepository.GetTaskById(taskdto.TaskId);

            task.TaskName = taskdto.TaskName;
            task.EquipmentName = taskdto.EquipmentName;
            task.DueDate = taskdto.DueDate;
            task.Priority = taskdto.Priority;
            task.Status = taskdto.Status;

            AuditModify(task);
            await MainTaskRepository.UpdateTask();

            var editedtaskdto = mapper.Map<MainTaskDTO>(task);

            await TaskHub.SendEditedTaskToClient(editedtaskdto, task.TechnicianId);

            return editedtaskdto;
        }

        public async Task UpdatePriority(int TaskId,PriorityEnum priority)
        {
            var task = await MainTaskRepository.GetTaskById(TaskId);
            task.Priority = priority;
            AuditModify(task);
            await MainTaskRepository.UpdateTask();
        }

        public async Task<MainTaskDTO> ChangeTaskStatus(int TaskId, StatusEnum status)
        {
            var task = await MainTaskRepository.GetTaskById(TaskId);

            task.Status = status;
            
            AuditModify(task);

            await MainTaskRepository.UpdateTask();

            var taskdto = mapper.Map<MainTaskDTO>(task);

            await TaskHub.SendChangeTaskStatusToClient(taskdto,task.TechnicianId,currentUser.Role);

            return taskdto;
        }

        public async Task OverDueTask()
        {
            var task = await MainTaskRepository.GetAllTask();

            if (task == null) return;

            foreach (MainTask mainTask in task.ToList())
            {
                if(mainTask.DueDate < DateOnly.FromDateTime(DateTime.Now) && mainTask.Status != StatusEnum.Completed)
                {
                    mainTask.Status = StatusEnum.OverDue;

                    await MainTaskRepository.UpdateTask();
                }
            }

        }
        public async Task<MainTaskDTO> AssignTechnician(int TaskId, string? TechId)
        {
            var task = await MainTaskRepository.GetTaskById(TaskId);

            if (task.TechnicianId != null)
            {
                var taskdto1 = mapper.Map<MainTaskDTO>(task);

                await TaskHub.SendRemoveAssignTaskToClient(taskdto1, task.TechnicianId);
            }

            if (TechId != null)
            {
               
                task.TechnicianId = TechId;
            }
            else if (TechId == null)
            {
                task.TechnicianId = null;

            }
            
            AuditModify(task);

            await MainTaskRepository.UpdateTask();

            var task1 = await MainTaskRepository.GetTaskById(TaskId);

            var taskdto = mapper.Map<MainTaskDTO>(task1);

            await TaskHub.SendUpdatedAssignTaskToClient(taskdto, TechId);

            return taskdto;
        }
        
        public async Task UnAssignTechnicianTaskUponDeletion(string TechId)
        {
            await MainTaskRepository.UnAssignTechnicianTask(TechId);
        }

    }
}
