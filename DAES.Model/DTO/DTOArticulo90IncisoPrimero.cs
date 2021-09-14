using DAES.Model.SistemaIntegrado;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DAES.Model.DTO
{
    public class DTOArticulo90IncisoPrimero : DTOSolicitante
    {
        [Required(ErrorMessage = "Es necesario especificar el dato Organización")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Balance general clasificado")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase BalanceGeneralClasificado { get; set; }

        [Display(Name = "Estado de resultados")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase EstadoDeResultados { get; set; }

        [Display(Name = "Balance ocho columnas")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase BalanceOchoColumnas { get; set; }

        [Display(Name = "Informe auditoría")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase InformdeAuditoria { get; set; }

        [Display(Name = "Estado de flujo de efectivo")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase EstadoDeFlujoDeEfectivo { get; set; }

        [Display(Name = "Notas explicativas de los Estados Financieros")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase NotasExplicativasDeLosEstadosFinancieros { get; set; }

        [Display(Name = "Certificado que acredite la inscripción de los auditores en el registro señalado en el articulo 89° de la R.A.E. 1321")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase CertificadoInscripcionAuditoria { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }
    }
}