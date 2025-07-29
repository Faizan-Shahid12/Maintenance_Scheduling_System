using Maintenance_Scheduling_System.Application.DTO.MainTaskDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class MainTaskController : ControllerBase
    {
        private IMainTaskService MainTaskService { get; set; }

        public MainTaskController(IMainTaskService service)
        {
            MainTaskService = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMainTask([FromQuery] int equipid,[FromBody] CreateMainTaskDTO MTdto)
        {
            await MainTaskService.CreateNewMainTask(equipid,MTdto);
            return Ok();
        }
    }
}
