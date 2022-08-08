using System;
using System.Drawing;
using System.IO;
using System.Linq;
using DAES.BLL.Interfaces;
using iTextSharp.text.pdf;
using QRCoder;
using TikaOnDotNet.TextExtraction;


namespace DAES.Infrastructure.File
{
    public class File : IFile
    {
        public Model.DTO.DTOFileMetadata BynaryToText(byte[] content)
        {
            if (content == null)
                return null;

            var textExtractor = new TextExtractor();
            var data = new Model.DTO.DTOFileMetadata();

            try
            {
                var extract = textExtractor.Extract(content);
                data.Type = extract.ContentType;
                data.Text = !string.IsNullOrWhiteSpace(extract.Text) ? extract.Text.Trim() : null;
                data.Metadata = extract.Metadata.Any() ? string.Join(";", extract.Metadata) : null;
            }
            catch (Exception)
            {
                return null;
            }

            return data;
        }

        public byte[] CreateQr(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    Bitmap qrCodeImage = qrCode.GetGraphic(20);
                    return ImageToByte(qrCodeImage);
                }
            }
        }

        private static byte[] ImageToByte(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public byte[] EstamparCodigoEnDocumento(byte[] documento, string text)
        {
            byte[] returnValue;

            if (documento == null)
                throw new Exception("Debe especificar el documento");

            using (MemoryStream ms = new MemoryStream())
            using (var reader = new PdfReader(documento))
            using (PdfStamper stamper = new PdfStamper(reader, ms, '\0', true))
            {
                try
                {
                    var pdfContent = stamper.GetOverContent(1);
                    var pagesize = reader.GetPageSize(1);
                    ColumnText.ShowTextAligned(pdfContent, iTextSharp.text.Element.ALIGN_MIDDLE, new iTextSharp.text.Phrase("ID " + text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)), 10, pagesize.Height - 20, 0);
                    stamper.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar código en documento:" + ex.Message);
                }

                stamper.Close();
                returnValue = ms.ToArray();
            }

            return returnValue;
        }
    }
}