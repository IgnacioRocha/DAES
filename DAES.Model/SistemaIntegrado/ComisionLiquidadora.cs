using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ComisionLiquidadora")]
    public class ComisionLiquidadora
    {
        public ComisionLiquidadora()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ComisionLiquidadoraId { get; set; }

        public int? DisolucionId { get; set; }
        public virtual Disolucion Disolucion { get; set; }

        public int? OrganizacionId { get; set; }
        /*public virtual Organizacion Organizacion { get; set; }*/
        public int? DirectorioId { get; set; }
        public virtual Directorio Directorio { get; set; }

        [Display(Name="RUN")]
        public string Rut { get; set; }

        [Display(Name ="Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Display(Name ="Cargo")]
        public int? CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        [Display(Name ="Genero")]
        public int? GeneroId { get; set; }
        public virtual Genero Genero { get; set; }

        [Display(Name ="Fecha de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInicio { get; set; }
        
        [Display(Name ="Fecha de termino")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaTermino { get; set; }
        
        [Display(Name ="Es Miembro?")]
        public bool EsMiembro { get; set; }

    }
}
