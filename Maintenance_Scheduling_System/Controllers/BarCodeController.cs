using Maintenance_Scheduling_System.Application.CQRS.BarCodeManager.Commands;
using Maintenance_Scheduling_System.Application.CQRS.BarCodeManager.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Drawing;
using System.Reflection.Metadata;
using System.Xml.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maintenance_Scheduling_System.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class BarCodeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly string _webRoot;

        public BarCodeController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _webRoot = env.WebRootPath;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> GenerateQRCode([FromBody] List<int> equipIds)
        {
            if (equipIds == null || equipIds.Count == 0)
                return BadRequest("No equipment IDs provided.");

            var barcodes = await _mediator.Send(new GenerateQRCodeQuery(equipIds));

            if (barcodes == null || barcodes.Count == 0)
                return NotFound("No barcodes could be generated.");

            using var doc = new PdfDocument();

            foreach (var barcode in barcodes)
            {
                if (barcode?.BarCode != null && barcode.BarCode.Length > 0)
                {
                    var page = doc.AddPage();
                    using var gfx = XGraphics.FromPdfPage(page);

                    gfx.DrawString(
                        $"Equipment: {barcode.EquipmentName} (Model: {barcode.EquipmentModel})",
                        new XFont("Arial", 14, XFontStyle.Regular),
                        XBrushes.Black,
                        new XRect(20, 20, page.Width - 40, 40),
                        XStringFormats.TopLeft);

                    using var ms = new MemoryStream(barcode.BarCode);
                    using var img = XImage.FromStream(() => ms);

                    double maxWidth = 300;
                    double maxHeight = 300;

                    double ratioX = maxWidth / img.PixelWidth;
                    double ratioY = maxHeight / img.PixelHeight;

                    double ratio = Math.Min(ratioX, ratioY);

                    double width = img.PixelWidth * ratio;
                    double height = img.PixelHeight * ratio;

                    gfx.DrawImage(img, 20, 70, width, height);


                }
            }

            using var outputStream = new MemoryStream();
            doc.Save(outputStream, false);

            var fileName = $"barcodes_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf";
            return File(outputStream.ToArray(), "application/pdf", fileName);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Technician")]
        public async Task<IActionResult> ReadQRCode([FromQuery] string base64)
        {
            var EquipDto = await _mediator.Send(new ReadQRCodeCommand(base64));

            if (EquipDto == null)
                return NotFound(new { message = "Equipment not found for given barcode" });

            return Ok(EquipDto);
        }
    }
}
