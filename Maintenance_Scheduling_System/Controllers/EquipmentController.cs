using Maintenance_Scheduling_System.Application.DTO.EquipmentDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService EquipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            EquipmentService = equipmentService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEquipment([FromBody] CreateEquipmentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await EquipmentService.CreateEquipment(dto);
            return Ok("Equipment created successfully.");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEquipment([FromBody] EquipmentDTO dto)
        {
            if (dto.EquipmentId == null)
                return BadRequest("Equipment ID is required.");

            await EquipmentService.UpdateEquipment(dto);
            return Ok("Equipment updated successfully.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            if (id == null)
                return BadRequest("Equipment ID is required.");

            await EquipmentService.DeleteEquipment(id);
            return Ok("Equipment soft-deleted.");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllEquipments()
        {
            var result = await EquipmentService.GetAllEquipments();
            return Ok(result);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetArchivedEquipments()
        {
            var result = await EquipmentService.GetArchivedEquipments();
            return Ok(result);
        }

        [HttpGet("{name}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEquipmentByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required.");

            var result = await EquipmentService.GetEquipmentByName(name);
            if (result == null || !result.Any())
                return NotFound("No equipment found with the given name.");

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEquipmentById(int id)
        {
            var result = await EquipmentService.GetEquipmentById(id);
            if (result == null)
                return NotFound($"No equipment found with ID {id}");

            return Ok(result);
        }

        [HttpPut("{equipId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignEquipType(int equipId, [FromQuery] string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return BadRequest("Type is required.");

            await EquipmentService.AssignEquipType(equipId, type);
            return Ok("Equipment type assigned.");
        }

        [HttpPut("{equipId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignWorkShopLocation(int equipId, [FromQuery] int workShopId)
        {
            if (workShopId <= 0)
                return BadRequest("Workshop ID must be greater than 0.");

            await EquipmentService.AssignWorkShopLocation(equipId, workShopId);
            return Ok("Workshop location assigned.");
        }

        [HttpPut("{equipId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ArchiveEquipment(int equipId)
        {
            await EquipmentService.ArchiveEquipment(equipId);
            return Ok("Equipment archived.");
        }

        [HttpPut("{equipId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnArchiveEquipment(int equipId)
        {
            await EquipmentService.UnArchiveEquipment(equipId);
            return Ok("Equipment unarchived.");
        }

    }
}
