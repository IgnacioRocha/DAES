using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;


//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DAES.Web.BackOffice.Controllers
{

    [Audit]
    [Authorize]
    public class ConfiguracionCertificadoController : Controller
    {
        //Se va a generar un DTO para crear un documento momentaneo y descargarlo
        public class DTODocumentTest
        {
            public DTODocumentTest() { }
            public byte[] Content { get; set; }
            public string FileName { get; set; }
            public byte[] File { get; set; }
            public string Metadata { get; set; }

            [DataType(DataType.MultilineText)]
            public string parrafo1 { get; set; }
        }

        private SistemaIntegradoContext db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        public ActionResult Index()
        {
            var configuracionCertificado = db.ConfiguracionCertificado.Include(c => c.TipoDocumento).Include(c => c.TipoOrganizacion)
                .OrderBy(q => q.TipoOrganizacion.Nombre)
                .ThenBy(q => q.TipoDocumento.Nombre);
            ViewBag.Certificado = configuracionCertificado.ToList();
            return View(configuracionCertificado.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfiguracionCertificado configuracionCertificado = db.ConfiguracionCertificado.Find(id);
            if (configuracionCertificado == null)
            {
                return HttpNotFound();
            }
            return View(configuracionCertificado);
        }

        public ActionResult Create()
        {
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre), "TipoDocumentoId", "Nombre");
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConfiguracionCertificadoId,Nombre,TipoDocumentoId,TipoOrganizacionId,Parrafo1,Parrafo2,Parrafo3,Titulo,Ciudad,Departamento,XML,IsActivo,TieneDirectorio,TieneEstatuto")] ConfiguracionCertificado configuracionCertificado)
        {
            if (ModelState.IsValid)
            {
                db.ConfiguracionCertificado.Add(configuracionCertificado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre), "TipoDocumentoId", "Nombre", configuracionCertificado.TipoDocumentoId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", configuracionCertificado.TipoOrganizacionId);
            return View(configuracionCertificado);
        }

        public async Task<ActionResult> ShowDoc(int id)
        {
            var model = await db.DocumentoConfiguracion.Where(q => q.ConfiguracionCertificadoId == id).FirstAsync();
            return File(model.Content, "application/pdf");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfiguracionCertificado configuracionCertificado = db.ConfiguracionCertificado.Find(id);
            if (configuracionCertificado == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre), "TipoDocumentoId", "Nombre", configuracionCertificado.TipoDocumentoId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", configuracionCertificado.TipoOrganizacionId);

            
            var documentoconfig = db.DocumentoConfiguracion.Where(q => q.ConfiguracionCertificadoId == id).FirstOrDefault();
            if (documentoconfig != null)
            {
                ViewBag.DocumentoConfiguracion = documentoconfig.ConfiguracionCertificadoId;
            }
            
            return View(configuracionCertificado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConfiguracionCertificadoId,Nombre,TipoDocumentoId,TipoOrganizacionId,Parrafo1,Parrafo2,Parrafo3,Parrafo4,Parrafo5,Titulo,Ciudad,Departamento,XML,IsActivo,TieneDirectorio," +
            "TieneEstatuto,Parrafo2ExAnterior,Parrafo2ExPosterior,Parrafo4ReAnterior,Parrafo4RePosterior,Parrafo1DisAnt,Parrafo1DisPos,ParrafoObservacion")] ConfiguracionCertificado configuracionCertificado)
        {

            if (ModelState.IsValid)
            {
                var doc_conf = db.DocumentoConfiguracion.Where(q => q.ConfiguracionCertificadoId == configuracionCertificado.ConfiguracionCertificadoId).FirstOrDefault();
                if (doc_conf == null)
                {
                    var nuevodoc = new DocumentoConfiguracion
                    {
                        Descripcion = "Configuracion para Certificado",
                        FileName = "CertificadoConfiguracion" + string.Format("{0:dd/MM/yyyy}", DateTime.Now) + ".pdf",
                        Content = _custom.CrearDocumentoConfiguracion(configuracionCertificado),
                        ConfiguracionCertificadoId = configuracionCertificado.ConfiguracionCertificadoId,
                    };
                    db.DocumentoConfiguracion.Add(nuevodoc);
                }
                else
                {
                    doc_conf.Content = _custom.CrearDocumentoConfiguracion(configuracionCertificado);
                }

                db.SaveChanges();

            }

            if (ModelState.IsValid)
            {
                db.Entry(configuracionCertificado).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                TempData["Message"] = Properties.Settings.Default.Success;
                return RedirectToAction("Edit", new { id = configuracionCertificado.ConfiguracionCertificadoId });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.TipoDocumentoId = new SelectList(db.TipoDocumento.OrderBy(q => q.Nombre), "TipoDocumentoId", "Nombre", configuracionCertificado.TipoDocumentoId);
            ViewBag.TipoOrganizacionId = new SelectList(db.TipoOrganizacion.OrderBy(q => q.Nombre), "TipoOrganizacionId", "Nombre", configuracionCertificado.TipoOrganizacionId);
            return View(configuracionCertificado);


        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfiguracionCertificado configuracionCertificado = db.ConfiguracionCertificado.Find(id);
            if (configuracionCertificado == null)
            {
                return HttpNotFound();
            }
            return View(configuracionCertificado);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConfiguracionCertificado configuracionCertificado = db.ConfiguracionCertificado.Find(id);
            db.ConfiguracionCertificado.Remove(configuracionCertificado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}