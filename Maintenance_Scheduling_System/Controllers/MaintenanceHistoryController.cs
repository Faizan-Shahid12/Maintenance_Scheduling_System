using Maintenance_Scheduling_System.Application.CQRS.CountManager.Queries;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.MaintenanceHistoryManager.Queries;
using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class MaintenanceHistoryController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MaintenanceHistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllMaintenanceHistory()
        {
            var result = await _mediator.Send(new GetAllMaintenanceHistoryQuery());
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTotalCount()
        {
            var counts = await _mediator.Send(new GetTotalCountQuery());
            return Ok(counts);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> GetMaintenanceHistoryByEquipmentId([FromQuery] int equipId)
        {
            var result = await _mediator.Send(new GetMaintenanceHistoryByEquipmentIdQuery(equipId));
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMaintenanceHistory([FromQuery] int historyId)
        {
            await _mediator.Send(new DeleteMaintenanceHistoryCommand(historyId));
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditMaintenanceHistory([FromBody] MaintenanceHistoryDTO dto)
        {
            await _mediator.Send(new EditMaintenanceHistoryCommand(dto));
            return Ok();
        }
    }
}
