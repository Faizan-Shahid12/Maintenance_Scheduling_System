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

            var equip = await EquipmentService.CreateEquipment(dto);
            return Ok(equip);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEquipment([FromBody] EquipmentDTO dto)
        {
            if (dto.EquipmentId == null)
                return BadRequest("Equipment ID is required.");

           var equip = await EquipmentService.UpdateEquipment(dto);
            return Ok(equip);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEquipment([FromQuery] int equipid)
        {
            if (equipid == null)
                return BadRequest("Equipment ID is required.");

           var equip = await EquipmentService.DeleteEquipment(equipid);
            return Ok(equip);
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEquipmentByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required.");

            var result = await EquipmentService.GetEquipmentByName(name);
            if (result == null || !result.Any())
                return NotFound("No equipment found with the given name.");

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEquipmentById([FromQuery] int id)
        {
            var result = await EquipmentService.GetEquipmentById(id);
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

            var equip = await EquipmentService.AssignEquipType(equipId, type);
            return Ok(equip);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignWorkShopLocation([FromQuery] int equipId, [FromQuery] int workShopId)
        {
            
           var equip = await EquipmentService.AssignWorkShopLocation(equipId, workShopId);
            return Ok(equip);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ArchiveEquipment([FromQuery] int equipId)
        {
            var equip = await EquipmentService.ArchiveEquipment(equipId);
            return Ok(equip);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnArchiveEquipment([FromQuery] int equipId)
        {
           var equip = await EquipmentService.UnArchiveEquipment(equipId);
            return Ok(equip);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllWorkShops()
        {
            var result = await EquipmentService.GetAllWorkShops();

            if (result == null || !result.Any())
                return NotFound("No workshops found.");

            return Ok(result);
        }
    }
}
