using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ActualizacionSupervisor")]
    public class ActualizacionSupervisor
    {
        public ActualizacionSupervisor()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ActualizacionSupervisorId { get; set; }

        public int? SupervisorAuxiliarId { get; set; }
        public virtual SupervisorAuxiliar SupervisorAuxiliar { get; set; }

        [Display(Name = "Razon Social")]
        public string RazonSocial { get; set; }

        [Display(Name = "Tipo de Persona Jurídica")]
        public int? TipoPersonaJuridicaId { get; set; }
        public virtual TipoPersonaJuridica TipoPersonaJuridica { get; set; }

        [Display(Name = "RUT")]
        public string Rut { get; set; }

        [Display(Name = "Domicilio Legal")]
        public string DomicilioLegal { get; set; }

        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Display(Name = "Correo Electronico")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe especificar un correo válido")]
        public string CorreoElectronico { get; set; }

        public int? TipoOrganizacionId { get; set; }
        public virtual TipoOrganizacion TipoOrganizacion { get; set; }

        /*[Required(ErrorMessage = "Es necesario adjuntar un documento")]*/
        [NotMapped]
        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase DocumentoAdjuntoTest { get; set; }

        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public byte[] DocumentoAdjunto { get; set; }

        [NotMapped]
        public string RutSolicitante { get; set; }
        [NotMapped]
        public string NombreSolicitante { get; set; }
        [NotMapped]
        public string ApellidosSolicitante { get; set; }
        [NotMapped]
        public string MailSolicitante { get; set; }

        public bool Aprobado { get; set; }
        public int? ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }
    }
}
