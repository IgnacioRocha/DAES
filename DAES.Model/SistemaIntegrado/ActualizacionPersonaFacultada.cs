using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ActualizacionPersonaFacultada")]
    public class ActualizacionPersonaFacultada
    {
        public ActualizacionPersonaFacultada()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ActualizacionPersonaFacultadaId { get; set; }

        public int? PersonaFacultadaId { get; set; }
        public virtual PersonaFacultada PersonaFacultada { get; set; }

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
    }
}
