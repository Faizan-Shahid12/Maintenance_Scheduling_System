using Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.MainTaskManager.Queries;
using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Services;
using Maintenance_Scheduling_System.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class MainTaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MainTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewMainTask([FromQuery] int equipId, [FromBody] CreateMainTaskDTO dto)
        {
            var result = await _mediator.Send(new CreateNewMainTaskCommand(equipId, dto));
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _mediator.Send(new GetAllMainTasksQuery());
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTaskByEquipId([FromQuery] int equipId)
        {
            var result = await _mediator.Send(new GetMainTasksByEquipmentIdQuery(equipId));
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTaskByHistoryId([FromQuery] int historyId)
        {
            var result = await _mediator.Send(new GetMainTasksByHistoryIdQuery(historyId));
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTask([FromQuery] int taskId)
        {
            var result = await _mediator.Send(new DeleteTaskCommand(taskId));
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTask([FromBody] MainTaskDTO dto)
        {
            var result = await _mediator.Send(new UpdateTaskCommand(dto));
            return Ok(result);
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePriority([FromQuery] int taskId, [FromQuery] PriorityEnum priority)
        {
            await _mediator.Send(new UpdatePriorityCommand(taskId, priority));
            return Ok(new { message = "Priority updated successfully." });
        }

        [HttpPatch]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> ChangeStatus([FromQuery] int taskId, [FromQuery] StatusEnum status)
        {
            await _mediator.Send(new ChangeTaskStatusCommand(taskId, status));
            return Ok(new { message = "Status changed successfully." });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOverDueTasks()
        {
            var result = await _mediator.Send(new GetAllOverDueTasksQuery());
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCompletedTask()
        {
            var result = await _mediator.Send(new GetAllCompletedTasksQuery());
            return Ok(result);
        }

        [HttpPatch]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> CompleteTask([FromQuery] int taskId)
        {
            var result = await _mediator.Send(new ChangeTaskStatusCommand(taskId, StatusEnum.Completed));
            return Ok(result);
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignTechnician([FromQuery] int taskId, [FromQuery] string? techId)
        {
            var result = await _mediator.Send(new AssignTechnicianCommand(taskId, techId));
            return Ok(result);
        }
    }

}
