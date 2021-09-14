using System.ComponentModel.DataAnnotations;

namespace DAES.Model.DTO
{
    public class DTOSolicitante
    {
        public DTOSolicitante()
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

        [Required(ErrorMessage = "Es necesario especificar el dato Fono")]
        [Display(Name = "Fono (solo números)")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Solo se permiten números")]
        public string FonoSolicitante { get; set; }


        [Required(ErrorMessage = "Es necesario especificar el dato Email")]
        [Display(Name = "Email")]
        public string EmailSolicitante { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Región")]
        [Display(Name = "Región")]
        public int RegionSolicitanteId { get; set; }
    }
}
