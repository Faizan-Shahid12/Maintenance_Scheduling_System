using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class MainTaskLogController : ControllerBase
    {
        private readonly IMainTaskLogService MainTaskLogService;

        public MainTaskLogController(IMainTaskLogService taskLogService)
        {
            MainTaskLogService = taskLogService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskLog([FromBody] CreateTaskLogDTO dto)
        {
            await MainTaskLogService.CreateTaskLog(dto);
            return Ok("Task log created successfully.");
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<List<TaskLogDTO>>> GetAllLogs(int taskId)
        {
            var logs = await MainTaskLogService.GetAllTaskLog(taskId);
            return Ok(logs);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLog([FromBody] TaskLogDTO dto)
        {
            await MainTaskLogService.UpdateTaskLog(dto);
            return Ok("Task log updated successfully.");
        }

        [HttpDelete("{logId}")]
        public async Task<IActionResult> DeleteLog(int logId)
        {
            await MainTaskLogService.DeleteTaskLog(logId);
            return Ok("Task log deleted successfully.");
        }
    }
}

