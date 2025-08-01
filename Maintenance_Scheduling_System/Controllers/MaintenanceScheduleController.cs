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
        /// <summary>
        /// 
        /// Apply the logic to Add Existing Task to Schedules
        /// 
        /// </summary>

        private IMaintenanceScheduleService MaintenanceScheduleService {  get; set; }

        public MaintenanceScheduleController(IMaintenanceScheduleService maintenanceScheduleService)
        {
            MaintenanceScheduleService = maintenanceScheduleService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMaintenanceSchedule([FromBody] CreateMaintenanceScheduleDTO dto)
        {
            await MaintenanceScheduleService.CreateMaintenanceSchedule(dto);
            return Ok("Schedule created.");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMaintenanceSchedule([FromBody] MaintenanceScheduleDTO dto)
        {
            await MaintenanceScheduleService.UpdateMaintenanceSchedule(dto);
            return Ok("Schedule updated.");
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMaintenanceSchedule(int id)
        {
            await MaintenanceScheduleService.DeleteMaintenanceSchedule(id);
            return Ok("Schedule deleted.");
        }

        [HttpPost("{scheduleId:int}/task")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewTaskToSchedule(int scheduleId, [FromBody] CreateScheduleTaskDTO dto)
        {
            await MaintenanceScheduleService.AddNewTasktoSchedule(scheduleId, dto);
            return Ok("Task added to schedule.");
        }

        [HttpDelete("{scheduleId:int}/task/{taskId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTaskFromSchedule(int scheduleId, int taskId)
        {
            await MaintenanceScheduleService.DeleteTaskFromSchedule(scheduleId, taskId);
            return Ok("Task removed from schedule.");
        }

        [HttpGet("{equipmentId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetScheduleByEquipment(int equipmentId)
        {
            var result = await MaintenanceScheduleService.GetMaintenanceScheduleByEquipmentId(equipmentId);
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

        [HttpPut("{scheduleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateSchedule(int scheduleId)
        {
            try
            {
                await MaintenanceScheduleService.ActivateSchedule(scheduleId);
                return Ok(new { message = "Schedule activated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{scheduleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnActivateSchedule(int scheduleId)
        {
            try
            {
                await MaintenanceScheduleService.UnActivateSchedule(scheduleId);
                return Ok(new { message = "Schedule deactivated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
