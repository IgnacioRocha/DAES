using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ReformaAnterior")]
    public class ReformaAnterior
    {
    


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int IdReformaAnterior { get; set; }

        [Display(Name = "Fecha de Reforma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaReforma { get; set; }

        [Display(Name = "Tipo de Norma")]
        public int? TipoNormaId { get; set; }
        public virtual TipoNorma TipoNorma { get; set; }


        [Display(Name = "Fecha de Norma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNorma { get; set; }

        [Display(Name = "Numero de norma")]
        public int? NNorma { get; set; }

        
        [Display(Name = "Fecha Publicacion Diario Oficial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPublicDiario { get; set; }

        [Display(Name = "Datos generales del notario")]
        public string DatosNotario { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        
        

    }


}
