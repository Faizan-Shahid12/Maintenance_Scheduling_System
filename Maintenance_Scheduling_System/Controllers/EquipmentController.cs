using Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.EquipmentManager.Queries;
using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EquipmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEquipment([FromBody] CreateEquipmentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var equip = await _mediator.Send(new CreateEquipmentCommand(dto));
            return Ok(equip);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEquipment([FromBody] EquipmentDTO dto)
        {
            if (dto.EquipmentId == null)
                return BadRequest("Equipment ID is required.");

            var equip = await _mediator.Send(new UpdateEquipmentCommand(dto));

            return Ok(equip);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEquipment([FromQuery] int equipid)
        {
            var success = await _mediator.Send(new DeleteEquipmentCommand(equipid));

            return Ok(success);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllEquipments()
        {
            var result = await _mediator.Send(new GetAllEquipmentsQuery());
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetArchivedEquipments()
        {
            var result = await _mediator.Send(new GetArchivedEquipmentsQuery());

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEquipmentByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required.");

            var result = await _mediator.Send(new GetEquipmentByNameQuery(name));

            if (result == null || !result.Any())
                return NotFound("No equipment found with the given name.");

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEquipmentById([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetEquipmentByIdQuery(id));

            if (result == null)
                return NotFound($"No equipment found with ID {id}");

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignEquipType([FromQuery] int equipId, [FromQuery] string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return BadRequest("Type is required.");

            var equip = await _mediator.Send(new AssignEquipmentTypeCommand(equipId, type));
            return Ok(equip);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignWorkShopLocation([FromQuery] int equipId, [FromQuery] int workShopId)
        {
            var equip = await _mediator.Send(new AssignWorkshopCommand(equipId, workShopId));
            return Ok(equip);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ArchiveEquipment([FromQuery] int equipId)
        {
            var equip = await _mediator.Send(new ArchiveEquipmentCommand(equipId));
            return Ok(equip);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnArchiveEquipment([FromQuery] int equipId)
        {
            var equip = await _mediator.Send(new UnArchiveEquipmentCommand(equipId));
            return Ok(equip);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllWorkShops()
        {
            var result = await _mediator.Send(new GetAllWorkShopsQuery());

            if (result == null || !result.Any())
                return NotFound("No workshops found.");

            return Ok(result);
        }
    }
}
