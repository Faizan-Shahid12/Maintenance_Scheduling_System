using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Services;
using Maintenance_Scheduling_System.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class MainTaskController : ControllerBase
    {
        private IMainTaskService MainTaskService { get; set; }

        public MainTaskController(IMainTaskService service)
        {
            MainTaskService = service;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewMainTask([FromQuery] int equipid,[FromBody] CreateMainTaskDTO MTdto)
        {
            var task = await MainTaskService.CreateNewMainTask(equipid,MTdto);
            return Ok(task);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTasks()
        {
            var task = await MainTaskService.GetAllMainTask();
            return Ok(task);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTaskByEquipId([FromQuery] int EquipId)
        {
            var task = await MainTaskService.GetMainTaskByEquipmentId(EquipId);
            return Ok(task);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTaskByHistoryId([FromQuery] int HistoryId)
        {
            var task = await MainTaskService.GetMainTaskByHistoryId(HistoryId);
            return Ok(task);
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTask([FromQuery] int TaskId)
        {
            var task = await MainTaskService.DeleteTask(TaskId);
            return Ok(task);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTask([FromBody] MainTaskDTO taskDto)
        {
            var task = await MainTaskService.UpdateTask(taskDto);
            return Ok(task);
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePriority([FromQuery] int taskId, [FromQuery] PriorityEnum priority)
        {
            await MainTaskService.UpdatePriority(taskId, priority);
            return Ok(new { message = "Priority updated successfully." });
        }

        [HttpPatch]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> ChangeStatus([FromQuery] int taskId, [FromQuery] StatusEnum status)
        {
            await MainTaskService.ChangeTaskStatus(taskId, status);
            return Ok(new { message = "Status changed successfully." });
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOverDueTasks()
        {
            var task = await MainTaskService.GetAllOverDueTask();
            return Ok(task);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompletedTask()
        {
            var task = await MainTaskService.GetAllCompletedTask();
            return Ok(task);
        }
        [HttpPatch]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> CompleteTask([FromQuery] int TaskId)
        {
            var task = await MainTaskService.ChangeTaskStatus(TaskId,StatusEnum.Completed);
            return Ok(task);
        }
        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignTechnician([FromQuery] int TaskId, [FromQuery] string? TechId)
        {
            var task = await MainTaskService.AssignTechnician(TaskId, TechId);
            return Ok(task); 
        }
    }

}
