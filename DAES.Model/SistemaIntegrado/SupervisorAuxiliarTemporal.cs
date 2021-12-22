using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.SistemaIntegrado
{
    [Table("SupervisorAuxiliarTemporal")]
    public class SupervisorAuxiliarTemporal
    {
        public SupervisorAuxiliarTemporal()
        {
            RepresentanteLegals = new List<RepresentanteLegal>();
            EscrituraConstitucionModificaciones = new List<EscrituraConstitucion>();
            ExtractoAuxiliars = new List<ExtractoAuxiliar>();
            PersonaFacultadas = new List<PersonaFacultada>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        [Key]
        public int SupervisorAuxiliarTempId { get; set; }

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
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "Es necesario adjuntar un documento")]
        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public byte[] DocumentoAdjunto { get; set; }

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
