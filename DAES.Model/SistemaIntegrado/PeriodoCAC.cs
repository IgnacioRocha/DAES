using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("PeriodoCAC")]
    public class PeriodoCAC
    {

        [Key]
        [Display(Name = "Id del periodo")]
        public int PeriodoId { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
    }
}

