using Maintenance_Scheduling_System.Application.CQRS.MainTaskLogManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.MainTaskLogManager.Queries;
using Maintenance_Scheduling_System.Application.DTO.TaskLogDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class MainTaskLogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MainTaskLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> CreateTaskLog([FromBody] CreateTaskLogDTO dto)
        {
            var log = await _mediator.Send(new CreateTaskLogCommand(dto));
            return Ok(log);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<ActionResult<List<TaskLogDTO>>> GetAllLogs([FromQuery] int taskId)
        {
            var logs = await _mediator.Send(new GetAllTaskLogsQuery(taskId));
            return Ok(logs);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> UpdateLog([FromBody] TaskLogDTO dto)
        {
            var log = await _mediator.Send(new UpdateTaskLogCommand(dto));
            return Ok(log);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> DeleteLog([FromQuery] int logId)
        {
            var log = await _mediator.Send(new DeleteTaskLogCommand(logId));
            return Ok(log);
        }
    }
}

