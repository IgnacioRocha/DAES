using System;
using System.ComponentModel.DataAnnotations;
using DAES.Model.SistemaIntegrado;

namespace DAES.Model.DTO
{
    public class DTODirectorio
    {
        public DTODirectorio()
        {
        }

        [Display(Name = "Id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Id")]
        public int DirectorioId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Organización")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Nombre completo")]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Rut")]
        [Display(Name = "RUT")]
        [StringLength(13)]
        public string Rut { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Fecha de inicio")]
        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Fecha de término")]
        [Display(Name = "Fecha de término")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaTermino { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Cargo")]
        [Display(Name = "Cargo")]
        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        [Required(ErrorMessage = "Es necesario especificar el dato Género")]
        [Display(Name = "Género")]
        public int GeneroId { get; set; }
        public virtual Genero Genero { get; set; }

    }
}
