using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Solicitante")]
    public class Solicitante
    {
        public Solicitante()
        {
            Procesos = new HashSet<Proceso>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Editable(false)]
        [Display(Name = "Id")]
        public int SolicitanteId { get; set; }

        [Display(Name = "RUT")]
        public string Rut { get; set; }

        [Display(Name = "Nombres solicitante")]
        public string Nombres { get; set; }

        [Display(Name = "Apellidos solicitante")]
        public string Apellidos { get; set; }

        [Display(Name = "Fono")]
        public string Fono { get; set; }

        [Display(Name = "Email solicitante")]
        public string Email { get; set; }

        [Display(Name = "Cargo")]
        public string Cargo { get; set; }

        [Display(Name = "Región")]
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        public virtual ICollection<Proceso> Procesos { get; set; }
    }
}
