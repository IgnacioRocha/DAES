using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.SistemaIntegrado
{
    [Table("DocumentoConfiguracion")]
    public class DocumentoConfiguracion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "DocumentoConfiguracionId")]
        public int DocumentoConfiguracionId { get; set; }

        public byte[] Content { get; set; }

        public string Descripcion { get; set; }

        public string FileName { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? FechaCreacion { get; set; } = DateTime.Now;

        public int ConfiguracionCertificadoId { get; set; }

    }
}
