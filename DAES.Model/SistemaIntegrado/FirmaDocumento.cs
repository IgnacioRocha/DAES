using DAES.Model.SistemaIntegrado;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.FirmaDocumento
{
    [Table("FirmaDocumento")]
    public class FirmaDocumento
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int FirmaDocumentoId { get; set; }

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }

        [Display(Name = "Autor")]
        public string Autor { get; set; }

        [Display(Name = "Fecha creación")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? FechaCreacion { get; set; } = DateTime.Now;

        [Display(Name = "Firmante")]
        public string Firmante { get; set; }

        [Display(Name = "Fecha firma")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? FechaFirma { get; set; }

        [Display(Name = "Documento a firmar")]
        public byte[] DocumentoSinFirma { get; set; }

        [Display(Name = "Documento a firmar")]
        public string DocumentoSinFirmaFilename { get; set; }


        [Display(Name = "Documento firmado")]
        public byte[] DocumentoConFirma { get; set; }

        [Display(Name = "Documento firmado")]
        public string DocumentoConFirmaFilename { get; set; }

        [Display(Name = "Folio")]
        public string Folio { get; set; }

        [Display(Name = "Código tipo documento")]
        public string TipoDocumentoCodigo { get; set; }

        [Display(Name = "Tipo documento")]
        public string TipoDocumentoDescripcion { get; set; }

        [Display(Name = "Código de barra")]
        public byte[] BarCode { get; set; }

        [Display(Name = "Firmado?")]
        public bool Firmado { get; set; }

        [Display(Name = "Proceso")]
        public int ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }

        [Display(Name = "Workflow")]
        public int WorkflowId { get; set; }

        public virtual Workflow Workflow { get; set; }
        //public virtual Core.Workflow Workflow { get; set; }

        [NotMapped]
        public bool TieneFirma { get; set; }

        [Display(Name = "URL gestión documental")]
        [DataType(DataType.Url, ErrorMessage = "Debe indicar una URL válida")]
        public string URL { get; set; }
        public int DocumentoId { get; set; }

        public DateTime Fecha { get; set; }

        public string Email { get; set; }

        public bool Signed { get; set; }

        public int TipoPrivacidadId { get; set; }

        public int TipoDocumentoId { get; set; }

        public int MyProperty { get; set; }

        public byte[] File { get; set; }

        [Display(Name = "Nombre")]
        public string FileName { get; set; }

        public string Texto { get; set; }

        public string Metadata { get; set; }

        public string Type { get; set; }
    }
}
