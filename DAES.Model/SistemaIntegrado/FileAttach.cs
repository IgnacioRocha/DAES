using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAES.Model.SistemaIntegrado
{
    public class FileAttach
    {
        [Display(Name ="Documento Adjunto")]
        public HttpPostedFileBase DocumentoAdjunto { get; set; }        
    }
}
