using Maintenance_Scheduling_System.Application.DTO.MaintenanceScheduleDTOs;
using Maintenance_Scheduling_System.Application.DTO.ScheduleTaskDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance_Scheduling_System.API.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class MaintenanceScheduleController : ControllerBase
    {
      
        private IMaintenanceScheduleService MaintenanceScheduleService {  get; set; }

        public MaintenanceScheduleController(IMaintenanceScheduleService maintenanceScheduleService)
        {
            MaintenanceScheduleService = maintenanceScheduleService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMaintenanceSchedule([FromBody] CreateMaintenanceScheduleDTO dto)
        {
            var schedule = await MaintenanceScheduleService.CreateMaintenanceSchedule(dto);
            return Ok(schedule);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMaintenanceSchedule([FromBody] MaintenanceScheduleDTO dto)
        {
            var schedule = await MaintenanceScheduleService.UpdateMaintenanceSchedule(dto);
            return Ok(schedule);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMaintenanceSchedule([FromQuery] int ScheduleId)
        {
            var schedule = await MaintenanceScheduleService.DeleteMaintenanceSchedule(ScheduleId);
            return Ok(schedule);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewTaskToSchedule([FromQuery] int ScheduleId, [FromBody] CreateScheduleTaskDTO dto)
        {
            var schedule = await MaintenanceScheduleService.AddNewTasktoSchedule(ScheduleId, dto);
            return Ok(schedule);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTaskFromSchedule([FromQuery] int ScheduleId, [FromQuery] int taskId)
        {
            var schedule = await MaintenanceScheduleService.DeleteTaskFromSchedule(ScheduleId, taskId);
            return Ok(schedule);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetScheduleByEquipment(int EquipId)
        {
            var result = await MaintenanceScheduleService.GetMaintenanceScheduleByEquipmentId(EquipId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllMaintenanceSchedules()
        {
            var result = await MaintenanceScheduleService.GetAllMaintenanceSchedule();
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSortedByStartDate()
        {
            var result = await MaintenanceScheduleService.GetAllMaintenanceScheduleByStartDate();
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateSchedule([FromQuery] int ScheduleId)
        {
            try
            {
                var schedule = await MaintenanceScheduleService.ActivateSchedule(ScheduleId);
                return Ok(schedule);

            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnActivateSchedule([FromQuery] int ScheduleId)
        {
            try
            {
                var schedule = await MaintenanceScheduleService.UnActivateSchedule(ScheduleId);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditScheduleTask([FromQuery] int ScheduleId, [FromBody] ScheduleTaskDTO ScheduleTaskDTO)
        {
            try
            {
                var schedule = await MaintenanceScheduleService.EditScheduleTask(ScheduleId,ScheduleTaskDTO);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignTechnicianToScheduleTask([FromQuery] int ScheduleId,[FromQuery] int ScheduleTaskId, [FromQuery] string? techId)
        {
            try
            {
                var schedule = await MaintenanceScheduleService.AssignTechnicianToScheduleTask(ScheduleId,ScheduleTaskId,techId);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
