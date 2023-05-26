using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace DAES.Model.DTO
{
    public class DTOObligacionPublicacionAnual
    {
        [Required(ErrorMessage = "Es necesario especificar el dato Documento")]
        [Display(Name = "Documento")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Año")]
        [Display(Name = "Año")]
        public int Periodo { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Rol")]
        [Display(Name = "Rol")]
        public int Rol { get; set; }

        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Es necesario especificar el dato Archivo")]
        [Display(Name = "Archivo")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }
    }

    public class ResultadoFormulario
    {
        public int ID;
        public bool Error;
        public string Mensaje;
    }

}