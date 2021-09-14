using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Linq;
using DAES.Infrastructure;
using DAES.Infrastructure.SistemaIntegrado;

namespace DAES.BLL
{
    public class EventoTitulos : PdfPageEventHelper
    {
        protected PdfTemplate total;
        protected BaseFont helv;

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            total = writer.DirectContent.CreateTemplate(100, 100);
            total.BoundingBox = new Rectangle(-20, -20, 100, 100);
            helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            var db = new SistemaIntegradoContext();
            var configurlLogo = db.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.URLImagenLogo);
            if (configurlLogo == null)
                throw new Exception("No se encontró la configuración de url de logo.");
            if (configurlLogo != null && configurlLogo.Valor.IsNullOrWhiteSpace())
                throw new Exception("La configuración de url de logo es inválida.");

            Image imagen = Image.GetInstance(configurlLogo.Valor);
            imagen.Alignment = Element.ALIGN_LEFT;
            imagen.SetAbsolutePosition(40, 670);
            imagen.ScalePercent(20);

            PdfContentByte cb = writer.DirectContent;
            cb.SaveState();
            cb.RestoreState();
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;
            cb.SaveState();
            cb.BeginText();
            cb.SetFontAndSize(helv, 7);
            string sPiePagina = "";

            float textSize = 7;
            float textBase = 200; // Este lo pone la informacion en la parte inferior

            sPiePagina = "______________________________________________________________________________________________________________________";
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, sPiePagina, 310, 50, 0);
            cb.AddTemplate(total, document.Right + textSize, textBase);


            var db = new SistemaIntegradoContext();
            var configLinea1 = db.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.DireccionLinea1);
            if (configLinea1 == null)
                throw new Exception("No se encontró la configuración la línea 1 de dirección.");
            if (configLinea1 != null && configLinea1.Valor.IsNullOrWhiteSpace())
                throw new Exception("La configuración la línea 1 de dirección es inválida.");

            var configLinea2 = db.Configuracion.FirstOrDefault(q => q.ConfiguracionId == (int)Infrastructure.Enum.Configuracion.DireccionLinea2);
            if (configLinea2 == null)
                throw new Exception("No se encontró la configuración la línea 2 de dirección.");
            if (configLinea2 != null && configLinea2.Valor.IsNullOrWhiteSpace())
                throw new Exception("La configuración la línea 2 de dirección es inválida.");

            sPiePagina = configLinea1.Valor;
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, sPiePagina, 310, 32, 0);
            cb.AddTemplate(total, document.Right + textSize, textBase);

            sPiePagina = configLinea2.Valor;
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, sPiePagina, 310, 22, 0);

            cb.EndText();
            cb.AddTemplate(total, document.Right + textSize, textBase);
            cb.RestoreState();
        }
    }
}
