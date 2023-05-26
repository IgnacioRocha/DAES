using System.ComponentModel.DataAnnotations;

namespace DAES.Model.FirmaDocumento
{
    public class DTOSolicitud
    {
        [Display(Name = "Status")]
        public string status { get; set; }

        [Display(Name = "Error")]
        public string error { get; set; }

        [Display(Name = "Periodo")]
        public string periodo { get; set; }

        [Display(Name = "Tipo documento")]
        public string tipodocumento { get; set; }

        [Display(Name = "Solicitante")]
        public string solicitante { get; set; }

        [Display(Name = "Folio")]
        public string folio { get; set; }

        [Display(Name = "Subsecretaría")]
        public string subsecretaria { get; set; }
    }
}
