using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ModificacionEstatuto")]
    public class ModificacionEstatuto
    {
        public ModificacionEstatuto()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ModificacionEstatutoId { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Fecha { get; set; }

        [Display(Name = "Número oficio")]
        public string NumeroOficio { get; set; }

        [Display(Name = "Vigente?")]
        public bool Vigente { get; set; } = false;
    }
}
