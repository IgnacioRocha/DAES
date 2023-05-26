using DAES.Model.SistemaIntegrado;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAES.Model.DTO
{
    [Table("SupervisorAuxiliar")]
    public class DTOSupervisorAuxiliar
    {
        public DTOSupervisorAuxiliar()
        {
            RepresentanteLegals = new List<RepresentanteLegal>();
            EscrituraConstitucionModificaciones = new List<EscrituraConstitucion>();
            ExtractoAuxiliars = new List<ExtractoAuxiliar>();
            PersonaFacultadas = new List<PersonaFacultada>();
        }

        public object SupervisorAuxiliar { get; set; }

        [Display(Name = "Razon Social")]
        public string RazonSocial { get; set; }

        [Display(Name = "RUT")]
        public string Rut { get; set; }

        [Display(Name = "Domicilio Legal")]
        public string DomicilioLegal { get; set; }

        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Display(Name = "Correo Electronico")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe especificar un correo válido")]
        public string CorreoElectronico { get; set; }

        [Display(Name = "Tipo de Persona Jurídica")]
        public int? TipoPersonaJuridicaId { get; set; }
        public virtual TipoPersonaJuridica TipoPersonaJuridica { get; set; }

        public bool Aprobado { get; set; }
        public int? ProcesoId { get; set; }

        [Key]
        [Display(Name="Id")]
        public int SupervisorAuxiliarId { get; set; }

        [Required(ErrorMessage ="Debe Adjuntar un documento")]
        [Display(Name ="Documento")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase DocumentoAdjunto { get; set; }

        [Display(Name ="Rut Solicitante")]
        public string RutSolicitante { get; set; }

        [Display(Name = "Nombres Solicitante: ")]
        public string NombresSolicitante { get; set; }

        [Display(Name = "Apellidos Solicitante: ")]
        public string ApellidosSolicitante { get; set; }

        [Display(Name = "*Email Soliciante")]
        [Required(ErrorMessage = "Debe especificar el email del solicitante")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe especificar un correo válido")]
        public string MailSolicitante { get; set; }

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
