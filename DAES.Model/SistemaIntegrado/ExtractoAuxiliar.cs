using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ExtractoAuxiliar")]
    public class ExtractoAuxiliar
    {
        public ExtractoAuxiliar()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Id")]
        public int ExtractoAuxiliarId { get; set; }

        [Display(Name ="Conservador de Comercio")]
        public string ConservadorComercio { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name ="Fecha de Inscripción")]
        public DateTime? FechaInscripcion { get; set; }

        [Display(Name ="Foja")]
        public string Foja { get; set; }
        
        [Display(Name ="Número")]
        public int? Numero { get; set; }
        
        [Display(Name ="Año")]
        public int? Año { get; set; }
                
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name ="Fecha de Publicación en el Diario Oficial")]
        public DateTime? FechaPublicacionDiarioOficial { get; set; }

        [Display(Name ="Numero de Publicación en el Diario Oficial")]
        public int? NumeroPublicacionDiarioOficial { get; set; }

        public int SupervisorAuxiliarId { get; set; }

    }
}