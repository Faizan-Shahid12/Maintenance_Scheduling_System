using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> CreateTaskLog([FromBody] CreateTaskLogDTO dto)
        {
            var log = await MainTaskLogService.CreateTaskLog(dto);
            return Ok(log);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<ActionResult<List<TaskLogDTO>>> GetAllLogs([FromQuery] int taskId)
        {
            var logs = await MainTaskLogService.GetAllTaskLog(taskId);
            return Ok(logs);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> UpdateLog([FromBody] TaskLogDTO dto)
        {
            var log = await MainTaskLogService.UpdateTaskLog(dto);
            return Ok(log);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> DeleteLog([FromQuery] int logId)
        {
            var log = await MainTaskLogService.DeleteTaskLog(logId);
            return Ok(log);
        }
    }
}

