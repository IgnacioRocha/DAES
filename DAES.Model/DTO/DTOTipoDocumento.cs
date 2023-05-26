using System.ComponentModel.DataAnnotations;

namespace DAES.Model.FirmaDocumento
{
    public class DTOTipoDocumento
    {
        [Display(Name = "ID")]
        public int TipoDocumentoId { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = "Habilitado?")]
        public bool Habilitado { get; set; }
    }
}