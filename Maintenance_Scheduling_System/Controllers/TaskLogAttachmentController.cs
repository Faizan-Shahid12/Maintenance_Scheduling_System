using Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.TaskLogAttachmentManager.Queries;
using Maintenance_Scheduling_System.Application.DTO.AttachmentDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class TaskLogAttachmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskLogAttachmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> UploadAttachment([FromForm] UploadAttachmentDTO attach)
        {
            var result = await _mediator.Send(new UploadAttachmentCommand(attach.LogId, attach.File));
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> DownloadAttachment([FromQuery] int attachId)
        {
            var attachment = await _mediator.Send(new DownloadAttachmentQuery(attachId));

            if (attachment == null)
                return NotFound("Attachment not found.");

            // Web root path
            var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var fullPath = Path.Combine(webRoot, attachment.FilePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (!System.IO.File.Exists(fullPath))
                return NotFound("File not found on server.");

            // Public URL
            var fileUrl = $"{Request.Scheme}://{Request.Host}/{attachment.FilePath.Replace("\\", "/")}";
            return Ok(new { url = fileUrl });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> DeleteAttachment([FromQuery] int attachId)
        {
            var result = await _mediator.Send(new DeleteAttachmentCommand(attachId));
            return Ok(result);
        }

        [HttpDelete("ByLog")]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> DeleteAttachmentsByLog([FromQuery] int logId)
        {
            await _mediator.Send(new DeleteAttachmentsByLogCommand(logId));
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> GetAllAttachments([FromQuery] int logId)
        {
            var result = await _mediator.Send(new GetAllAttachmentsQuery(logId));
            return Ok(result);
        }
    }
}
