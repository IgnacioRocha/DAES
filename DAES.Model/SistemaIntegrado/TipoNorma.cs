using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("TipoNorma")]
    public class TipoNorma
    {
        public TipoNorma()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int TipoNormaId { get; set; }

        [Required(ErrorMessage ="Es necesario Especificar este dato")]
        [Display(Name ="Tipo Documento")]
        public string Nombre { get; set; }

        /*[Display(Name ="Fecha de Norma")]
        public DateTime FechaNorma { get; set; }*/


    }
}
