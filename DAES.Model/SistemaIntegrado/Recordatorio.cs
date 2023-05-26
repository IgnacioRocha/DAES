using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Recordatorio")]
    public class Recordatorio
    {
        public Recordatorio()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordatorioId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Resuelto?")]
        public bool Resuelto { get; set; } = false;
    }
}
