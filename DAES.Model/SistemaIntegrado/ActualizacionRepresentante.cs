using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAES.Model.SistemaIntegrado
{

    [Table("ActualizacionRepresentante")]
    public class ActualizacionRepresentante
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ActualizacionRepresentanteId { get; set; }

        public int? RepresentanteLegalId { get; set; }
        public virtual RepresentanteLegal RepresentanteLegal { get; set; }

        public int? ActualizacionSupervisorId { get; set; }
        public virtual ActualizacionSupervisor ActualizacionSupervisor { get; set; }

        public int? ProcesoId { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Display(Name = "RUN")]
        public string RUN { get; set; }

        [Display(Name = "Profesión")]
        public string Profesion { get; set; }

        [Display(Name = "Domicilio")]
        public string Domicilio { get; set; }

        [Display(Name = "Nacionalidad")]
        public string Nacionalidad { get; set; }

        public bool Eliminado { get; set; }
    }
}
