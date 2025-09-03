using Maintenance_Scheduling_System.Application.Interfaces;
using Maintenance_Scheduling_System.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.ImageSharp.Rendering;
using ZXing.QrCode;
using ZXing.Rendering;
using ZXing.Windows.Compatibility;

namespace Maintenance_Scheduling_System.Infrastructure.External_Libraries.BarCode
{
    public class BarCodeService : IBarCodeService
    {

        public string ReadQRCodeByFile(IFormFile file)
        {
            if (file == null || file.Length == 0) return string.Empty;

            using var stream = file.OpenReadStream();
            using var image = Image.Load<Rgba32>(stream);

            var options = new DecodingOptions
            {
                CharacterSet = "UTF-8",
                TryHarder = true,
                TryInverted = true,
                PossibleFormats = new[]
                {
                    BarcodeFormat.CODE_128,
                    BarcodeFormat.CODE_39,
                    BarcodeFormat.EAN_13,
                    BarcodeFormat.EAN_8,
                    BarcodeFormat.QR_CODE,
                    BarcodeFormat.ITF
                }
            };

            var reader = new ZXing.ImageSharp.BarcodeReader<Rgba32>
            {
                AutoRotate = true,
                Options = options
            };

            var result = reader.Decode(image);
            return result?.Text ?? string.Empty;
        }
        

        public byte[] GenerateQRCode(Guid text)
        {
            var options = new EncodingOptions
            {
                Height = 100,
                Width = 300,
                Margin = 2,

            };

            var barcodeWriter = new BarcodeWriter<Image<Rgba32>>
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options,
                Renderer = new ImageSharpRenderer<Rgba32>()
            };


            using var bitmap = barcodeWriter.Write(text.ToString());
            using var ms = new MemoryStream();
            bitmap.Save(ms, new PngEncoder());
            return ms.ToArray();
        }

        public string ReadQRCode(string Decodedbarcode)
        {
       
            try
            {
                if (Decodedbarcode.Contains(","))
                    Decodedbarcode = Decodedbarcode.Substring(Decodedbarcode.IndexOf(',') + 1);

                var bytes = Convert.FromBase64String(Decodedbarcode);
                using var ms = new MemoryStream(bytes);
                using var image = Image.Load<Rgba32>(ms);


                var options = new DecodingOptions
                {
                    CharacterSet = "UTF-8",
                    PureBarcode = false,
                    TryHarder = true,
                    TryInverted = true,
                    PossibleFormats = new[]
                    {
                        BarcodeFormat.CODE_128, BarcodeFormat.CODE_39,
                        BarcodeFormat.EAN_13, BarcodeFormat.EAN_8,
                        BarcodeFormat.QR_CODE, BarcodeFormat.ITF
                    }
                };

                var reader = new ZXing.ImageSharp.BarcodeReader<Rgba32>
                {
                    AutoRotate = true,
                    Options = options
                };

                var best = reader.Decode(image);

                // 2) Try multiple on original
                if (best == null)
                {
                    var many = reader.DecodeMultiple(image);
                    best = many?.OrderByDescending(r => r.Text?.Length ?? 0).FirstOrDefault();
                }

                // 3) Try upscaled then multiple
                if (best == null)
                {
                    using var img2x = image.Clone(x => x.Resize(image.Width * 2, image.Height * 2));
                    best = reader.Decode(img2x) ??
                           reader.DecodeMultiple(img2x)?.OrderByDescending(r => r.Text?.Length ?? 0).FirstOrDefault();
                }

                return best?.Text ?? string.Empty;
            }
            catch (ZXing.FormatException)
            {
                return string.Empty; // invalid base64
            }
            catch (SixLabors.ImageSharp.UnknownImageFormatException)
            {
                return string.Empty; // not a supported image (ensure screenshotFormat is png/jpeg)
            }
        }
    }
}
