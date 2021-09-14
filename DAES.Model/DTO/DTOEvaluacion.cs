using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAES.Model.DTO
{
    public class DTOEvaluacion
    {

        [Required(ErrorMessage = "Es necesario especificar el dato Periodo")]
        [Display(Name = "Periodo")]
        public int PeriodoId { get; set; }

        public List<DAES.Model.Supervision.cacEvaluacion> cacEvaluacions { get; set; }
    }
}