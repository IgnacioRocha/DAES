using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ExistenciaPosterior")]
    public class ExistenciaPosterior
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int? idExistenciaPost { get; set; }

        [Display(Name = "Fecha Constitutiva de socios")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaConstitutivaSocios { get; set; }

        [Display(Name = "Fecha de Escritura Pública")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEscrituraPublica { get; set; }

        [Display(Name = "Fecha de Publicación (Diario Oficial)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPublicacionn { get; set; }

        [Display(Name = "Datos Generales del Notario")]
        public string DatosGeneralesNotario { get; set; }

        [Display(Name = "Fojas/N°")]
        public string Fojas { get; set; }

        [Display(Name = "Año de inscripción")]
        public int? AnoInscripcion { get; set; }

        [Display(Name ="Datos CBR")]
        public string DatosCBR { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

    }
}
