using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ActualizacionOrganizacionDirectorio")]
    public class ActualizacionOrganizacionDirectorio
    {
        public ActualizacionOrganizacionDirectorio()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ActualizacionOrganizacionDirectorioId { get; set; }

        //[Required(ErrorMessage = "Es necesario especificar este dato")]
        //[Display(Name = "Organización")]
        //public int OrganizacionId { get; set; }
        //public virtual Organizacion Organizacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; }

        [Display(Name = "RUT")]
        [StringLength(13)]
        public string Rut { get; set; }

        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInicio { get; set; }

        [Display(Name = "Fecha de término")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaTermino { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Cargo")]
        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Género")]
        public int GeneroId { get; set; }
        public virtual Genero Genero { get; set; }
    }
}
