using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Articulo91")]
    public class Articulo91
    {
        public Articulo91() {
            //Documentos = new List<Documento>();
        }

        [Key]
        public int Articulo91Id { get; set; }

        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Fecha de publicación")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Es necesario especificar el Nombre del Gerente")]
        [Display(Name = "Nombre del Gerente")]
        public string NombreGerente { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el Nombre Contador General")]
        [Display(Name = "Nombre Contador General")]
        public string NombreContadorGeneral { get; set; }

        [Display(Name = "Proceso")]
        public int? ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }

        [Display(Name = "Periodo")]
        public string Periodo { get; set; }
        //public virtual Periodo Periodo { get; set; }

        [Display(Name = "Estado de aprobación")]
        public bool OK { get; set; }

        [Display(Name = "Fecha celebración última Junta General de Socios")]
        [DataType(DataType.Date)]
        public DateTime? FechaCelebracionUltimaJuntaGeneralSocios { get; set; }
    }
}
