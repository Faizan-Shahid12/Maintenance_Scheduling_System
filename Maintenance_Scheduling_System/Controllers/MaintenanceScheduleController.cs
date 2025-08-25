using Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceScheduleManager.Queries;
using Maintenance_Scheduling_System.Application.CQRS.ScheduleTaskManager.Commands;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance_Scheduling_System.API.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class MaintenanceScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaintenanceScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMaintenanceSchedule([FromBody] CreateMaintenanceScheduleDTO dto)
        {
            var result = await _mediator.Send(new CreateMaintenanceScheduleCommand(dto));
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMaintenanceSchedule([FromBody] MaintenanceScheduleDTO dto)
        {
            var result = await _mediator.Send(new UpdateMaintenanceScheduleCommand(dto));
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMaintenanceSchedule([FromQuery] int scheduleId)
        {
            var result = await _mediator.Send(new DeleteMaintenanceScheduleCommand(scheduleId));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewTaskToSchedule([FromQuery] int scheduleId, [FromBody] CreateScheduleTaskDTO dto)
        {
            var result = await _mediator.Send(new AddNewTaskToScheduleCommand(scheduleId, dto));
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTaskFromSchedule([FromQuery] int scheduleId, [FromQuery] int taskId)
        {
            var result = await _mediator.Send(new DeleteTaskFromScheduleCommand(scheduleId, taskId));
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetScheduleByEquipment([FromQuery] int equipId)
        {
            var result = await _mediator.Send(new GetMaintenanceSchedulesByEquipmentIdQuery(equipId));
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllMaintenanceSchedules()
        {
            var result = await _mediator.Send(new GetAllMaintenanceSchedulesQuery());
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSortedByStartDate()
        {
            var result = await _mediator.Send(new GetAllMaintenanceSchedulesByStartDateQuery());
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateSchedule([FromQuery] int scheduleId)
        {
            var result = await _mediator.Send(new ActivateScheduleCommand(scheduleId));
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnActivateSchedule([FromQuery] int scheduleId)
        {
            var result = await _mediator.Send(new UnActivateScheduleCommand(scheduleId));
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditScheduleTask([FromQuery] int scheduleId, [FromBody] ScheduleTaskDTO dto)
        {
            var result = await _mediator.Send(new EditScheduleTaskInScheduleCommand(scheduleId, dto));
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignTechnicianToScheduleTask([FromQuery] int scheduleId, [FromQuery] int scheduleTaskId, [FromQuery] string? techId)
        {
            var result = await _mediator.Send(new AssignTechnicianToTaskCommand(scheduleId, scheduleTaskId, techId));
            return Ok(result);
        }
    }
}
