using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Fiscalizacion")]
    public class Fiscalizacion
    {
        public Fiscalizacion()
        {
            Hallazgos = new List<Hallazgo>();
        }

        [Key]
        public int FiscalizacionId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo fiscalización")]
        public int? TipoFiscalizacionId { get; set; }
        public virtual TipoFiscalizacion TipoFiscalizacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Número ingreso")]
        public string NumeroIngreso { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Observación")]
        public string Observacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo hallazgo")]
        public int? TipoHallazgoId { get; set; }
        public virtual TipoHallazgo TipoHallazgo { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Tipo materia")]
        public int? TipoMateriaId { get; set; }
        public virtual TipoMateria TipoMateria { get; set; }

        [Display(Name = "Oficio anterior")]
        public string OficioAnterior { get; set; }

        [Display(Name = "Fecha oficio anterior")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaOficioAnterior { get; set; } = DateTime.Now;

        [Display(Name = "Oficio remitido")]
        public string OficioRemitido { get; set; }

        [Display(Name = "Fecha oficio remitido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaOficioRemitido { get; set; } = DateTime.Now;

        [Display(Name = "Tipo oficio")]
        public int? TipoOficioId { get; set; }
        public virtual TipoOficio TipoOficio { get; set; }

        [Display(Name = "N° de reiteración")]
        public int? NumeroReiteracion { get; set; }

        [Display(Name = "N° de Hallazgo pendientes")]
        public int? NumeroHallazgoPendientes { get; set; }

        [Display(Name = "Plazo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Plazo { get; set; } = DateTime.Now;

        [Display(Name = "Multa")]
        public bool Multa { get; set; } = false;

        public virtual List<Hallazgo> Hallazgos { get; set; }



        [Display(Name = "Responsable 1")]
        public string Responsable1 { get; set; }

        [Display(Name = "Responsable 2")]
        public string Responsable2 { get; set; }

        [Display(Name = "Responsable 3")]
        public string Responsable3 { get; set; }




        [Display(Name = "Proceso")]
        public int? ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }


        [Display(Name = "Proceso relacionado")]
        public int? ProcesoRelacionadoId { get; set; }
        [NotMapped]
        public virtual Proceso ProcesoRelacionado { get; set; }


        public bool Activo { get; set; } = true;
        public string EliminadoPor { get; set; }

    }
}
