using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ControlCambio")]
    public class ControlCambio
    {
        [Key]
        [Display(Name ="Id del Cambio")]
        public int IdCambio { get; set; }
        [Display(Name ="Id del Usuario")]
        public string UsuarioId { get; set; }

        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }
        [Display(Name = "Fecha del cambio")]
        public DateTime FechaCambio { get; set; }
        [Display(Name = " Razón Social de la Organización")]
        public string Organizacion { get; set; }

        [Display(Name ="Id de la Organización ")]
        public int OrganizacionId { get; set; }
    }
}
