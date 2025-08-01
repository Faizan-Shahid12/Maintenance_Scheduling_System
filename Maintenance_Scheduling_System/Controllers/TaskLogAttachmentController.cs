using Maintenance_Scheduling_System.Application.DTO.AttachmentDTOs;
using Maintenance_Scheduling_System.Application.Interfaces;
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
            await AttachmentService.UploadAttachment(attach.LogId, attach.File);
            return Ok("File uploaded successfully.");
        }

        [HttpGet("{attachId}")]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> DownloadAttachment(int attachId)
        {
            var attachment = await AttachmentService.DownloadAttachment(attachId);

            var fullPath = attachment.FilePath;

            if (!System.IO.File.Exists(fullPath))
                return NotFound("File not found on server.");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);

            return File(fileBytes, attachment.ContentType, attachment.FileName);
        }

        [HttpDelete("{attachId}")]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> DeleteAttachment(int attachId)
        {
            await AttachmentService.DeleteAttachment(attachId);
            return Ok("Attachment soft deleted.");
        }
    }
}
