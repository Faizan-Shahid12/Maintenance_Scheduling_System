using Maintenance_Scheduling_System.Application.DTO.MaintenanceHistoryDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class MaintenanceHistoryController : ControllerBase
    {

        private IMaintenanceHistoryService MaintenanceHistoryService;

        public MaintenanceHistoryController(IMaintenanceHistoryService service)
        {
            MaintenanceHistoryService = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllMaintenanceHistory()
        {
            return Ok(await MaintenanceHistoryService.GetAllMaintenanceHistory());
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> GetMaintenanceHistoryByEquipmentId([FromQuery] int EquipId)
        {
            var main = await MaintenanceHistoryService.GetMaintenanceHistoryByEquipmentId(EquipId);

            return Ok(main);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMaintenanceHistory([FromQuery] int HistoryId)
        {
            var main = MaintenanceHistoryService.DeleteHistory(HistoryId);
            return Ok();
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditMaintenanceHistory([FromBody] MaintenanceHistoryDTO DTO)
        {
             await MaintenanceHistoryService.EditHistory(DTO);
            return Ok();
        }
    }
}
