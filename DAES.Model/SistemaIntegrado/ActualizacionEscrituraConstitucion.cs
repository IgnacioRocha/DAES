using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ActualizacionEscrituraConstitucion")]
    public class ActualizacionEscrituraConstitucion
    {
        public ActualizacionEscrituraConstitucion()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ActualizacionEscrituraConstitucionId { get; set; }

        public int? EscrituraConstitucionId { get; set; }
        public virtual EscrituraConstitucion EscrituraConstitucion { get; set; }

        [Display(Name = "Notaría")]
        public string Notaria { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime? Fecha { get; set; }

        [NotMapped]
        public int? ProcesoId { get; set; }

        [Display(Name = "Numero de Repertorio")]
        public int? NumeroRepertorio { get; set; }
    }
}
