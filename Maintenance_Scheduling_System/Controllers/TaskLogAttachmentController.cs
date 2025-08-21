using Maintenance_Scheduling_System.Application.DTO.AttachmentDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class TaskLogAttachmentController : ControllerBase
    {
        private ITaskLogAttachmentService AttachmentService { get; set; }
        

        public TaskLogAttachmentController(ITaskLogAttachmentService attachmentService)
        {
            AttachmentService = attachmentService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> UploadAttachment([FromForm] UploadAttachmentDTO attach)
        {
            var attach1 = await AttachmentService.UploadAttachment(attach.LogId, attach.File);
            return Ok(attach1);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> DownloadAttachment([FromQuery] int attachId)
        {
            var attachment = await AttachmentService.DownloadAttachment(attachId);

            if (attachment == null)
                return NotFound("Attachment not found.");

            // Set webRoot path
            var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            // Build full physical path using stored relative path
            var fullPath = Path.Combine(webRoot, attachment.FilePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

            // Ensure file physically exists
            if (!System.IO.File.Exists(fullPath))
                return NotFound("File not found on server.");

            // Build public URL to access via browser
            var fileUrl = $"{Request.Scheme}://{Request.Host}/{attachment.FilePath.Replace("\\", "/")}";

            return Ok(new { url = fileUrl });
        }



        [HttpDelete]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> DeleteAttachment([FromQuery] int attachId)
        {
            var attach = await AttachmentService.DeleteAttachment(attachId);
            return Ok(attach);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> GetAllAttachments([FromQuery] int logId)
        {
            var attach = await AttachmentService.GetAllAttachments(logId);
            return Ok(attach);
        }
    }

}
