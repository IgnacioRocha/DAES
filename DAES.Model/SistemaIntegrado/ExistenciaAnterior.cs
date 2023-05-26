using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ExistenciaLegalAnterior")]
    public class ExistenciaAnterior
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int IdExistenciaAnterior { get; set; }

        [Display(Name = "Tipo Norma")]
        public int? TipoNormaId { get; set; }
        public virtual TipoNorma tipoNorma { get; set; }

        [Display(Name = "Número de Norma")]
        public int? NNorma { get; set; }

        [Display(Name = "Fecha de Norma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FNorma { get; set; }
        
        [Display(Name = "Fecha de publicación (Diario Oficial)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPublicacion { get; set; }

        [Display(Name = " Autorizado Por")]
        public string Autorizado { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }


    }
}
