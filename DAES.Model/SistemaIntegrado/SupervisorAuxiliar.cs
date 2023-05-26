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
    [Table("SupervisorAuxiliar")]
    public class SupervisorAuxiliar
    {
        public SupervisorAuxiliar()
        {
            RepresentanteLegals = new List<RepresentanteLegal>();
            EscrituraConstitucionModificaciones = new List<EscrituraConstitucion>();
            ExtractoAuxiliars = new List<ExtractoAuxiliar>();
            PersonaFacultadas = new List<PersonaFacultada>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="Id")]
        public int SupervisorAuxiliarId { get; set; }

        [Display(Name ="Razón Social")]
        public string RazonSocial { get; set; }

        [Display(Name ="Tipo de Persona Jurídica")]
        public int? TipoPersonaJuridicaId { get; set; }
        public virtual TipoPersonaJuridica TipoPersonaJuridica { get; set; }

        [Display(Name ="RUT")]
        public string Rut { get; set; }
        
        [Display(Name ="Domicilio Legal")]
        public string DomicilioLegal { get; set; }
        
        [Display(Name ="Teléfono")]
        public string Telefono { get; set; }
        
        [Display(Name ="Correo Electrónico")]
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

        /*Lista Representante Legal*/
        public virtual List<RepresentanteLegal> RepresentanteLegals { get; set; }

        /*Lista escritura constitucion y modificacion*/
        public virtual List<EscrituraConstitucion> EscrituraConstitucionModificaciones { get; set; }
        /*Lista Modificaciones*/

        /*Lista extracto*/
        public virtual List<ExtractoAuxiliar> ExtractoAuxiliars { get; set; }
        /*Lista personas facultadas de supervision*/
        public virtual List<PersonaFacultada> PersonaFacultadas { get; set; }        
    }
}
