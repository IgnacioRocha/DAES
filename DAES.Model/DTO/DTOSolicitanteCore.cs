using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.DTO
{
    public  class DTOSolicitanteCore
    {
        public DTOSolicitanteCore()
        {

        }

        [Required(ErrorMessage = "Es necesario especificar el dato RUT")]
        [Display(Name = "RUT (sin puntos y sin guión)")]
        public string RUTSolicitante { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Nombres")]
        [Display(Name = "Nombres")]
        public string NombresSolicitante { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Apellidos")]
        [Display(Name = "Apellidos")]
        public string ApellidosSolicitante { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Email Solicitante")]
        [Display(Name = "Email Solicitante")]
        public string EmailSolicitante { get; set; }

    }
}
