using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Services;
using Maintenance_Scheduling_System.Domain.Enums;
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
        public async Task<IActionResult> AddNewMainTask([FromQuery] int equipid,[FromBody] CreateMainTaskDTO MTdto)
        {
            await MainTaskService.CreateNewMainTask(equipid,MTdto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var task = await MainTaskService.GetAllMainTask();
            return Ok(task);
        }

        [HttpGet("{EquipId:int}")]
        public async Task<IActionResult> GetTaskByEquipId(int EquipId)
        {
            var task = await MainTaskService.GetMainTaskByEquipmentId(EquipId);
            return Ok(task);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask([FromQuery] int TaskId)
        {
            await MainTaskService.DeleteTask(TaskId);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] MainTaskDTO taskDto)
        {
            await MainTaskService.UpdateTask(taskDto);
            return Ok(new { message = "Task updated successfully." });
        }

        [HttpPatch("{taskId}")]
        public async Task<IActionResult> UpdatePriority(int taskId, [FromQuery] PriorityEnum priority)
        {
            await MainTaskService.UpdatePriority(taskId, priority);
            return Ok(new { message = "Priority updated successfully." });
        }

        [HttpPatch("{taskId}")]
        public async Task<IActionResult> ChangeStatus(int taskId, [FromQuery] StatusEnum status)
        {
            await MainTaskService.ChangeTaskStatus(taskId, status);
            return Ok(new { message = "Status changed successfully." });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOverDueTasks()
        {
            var task = await MainTaskService.GetAllOverDueTask();
            return Ok(task);
        }
        [HttpGet]
        public async Task<IActionResult> GetCompletedTask()
        {
            var task = await MainTaskService.GetAllCompletedTask();
            return Ok(task);
        }
        [HttpPatch]
        public async Task<IActionResult> CompleteTask([FromQuery] int TaskId)
        {
            await MainTaskService.ChangeTaskStatus(TaskId,StatusEnum.Completed);
            return Ok();
        }
        [HttpPatch]
        public async Task<IActionResult> AssignTechnician([FromQuery] int TaskId, [FromQuery] string TechId)
        {
            await MainTaskService.AssignTechnician(TaskId, TechId);
            return Ok(); 
        }
    }

}
