using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ReformaPosterior")]
    public class ReformaPosterior
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int IdReformaPost { get; set; }

        [Display(Name = "Fecha de Reforma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FReforma { get; set; }

        
        [Display(Name = "Fecha junta General de Socios")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaJuntGeneralSocios { get; set; }

       
        [Display(Name = "Fecha de Escritura Pública")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEscrituraPublica { get; set; }

        [Display(Name = "Fecha de públicacion (diario oficial)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPubliDiario { get; set; }

        [Display(Name = "Año inscripción")]
        public string AnoInscripcion { get; set; }

        [Display(Name = "Fojas Número")]
        public string FojasNumero { get; set; }
        
        [Display(Name = "Datos CBR")]
        public string DatosCBR { get; set; }

        [Display(Name = "Datos generales del notario Público y Notaría")]
        public string DatosGeneralNotario { get; set; }

        //esto sirve para agregar espacios al documento.
        [Display(Name = "texto Adicional")]
        [DataType(DataType.MultilineText)]
        public string EspaciosDoc { get; set; }

        [Display(Name = "Número del Oficio")]
        public string NumeroOficio { get; set; }

        [Display(Name = "Fecha del Oficio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaOficio { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        
    }
}
