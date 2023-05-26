using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ActaFiscalizacion")]
    public class ActaFiscalizacion
    {
        public ActaFiscalizacion()
        {
            ActaFiscalizacionHechoContables = new List<ActaFiscalizacionHechoContable>();
            ActaFiscalizacionHechoLegals = new List<ActaFiscalizacionHechoLegal>();
            ActaFiscalizacionFiscalizadorContables = new List<ActaFiscalizacionFiscalizadorContable>();
            ActaFiscalizacionFiscalizadorLegals = new List<ActaFiscalizacionFiscalizadorLegal>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ActaFiscalizacionId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "FechaCreacion")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "N° Oficio de Acreditación y Requerimientos")]
        [DataType(DataType.MultilineText)]
        public string NOficioAcreditacioRequerimientos { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Salida Oficio de Acreditación y Requerimientos")]
        [DataType(DataType.Date)]
        public DateTime FechaSalidaOficioAcreditacionRequerimientos { get; set; }

        [Display(Name = "N° Acta de Reunión de Fiscalización In Situ")]
        [DataType(DataType.MultilineText)]
        public string NActaReunionFiscalizacionInSitu { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Fiscalización In Situ")]
        [DataType(DataType.Date)]
        public DateTime FechaFiscalizacionInSitu { get; set; }

        [Display(Name = "Registro Único Tributario")]
        public string RUT { get; set; }

        [Display(Name = "Representante Legal")]
        public string RepresentanteLegal { get; set; }

        [Display(Name = "Género Representante Legal")]
        [ForeignKey("GeneroRepresentanteLegal"), Column(Order = 0)]
        public int? GeneroRepresentanteLegalId { get; set; }
        public virtual Genero GeneroRepresentanteLegal { get; set; }

        [Display(Name = "Vigencia Representante Legal")]
        public bool VigenciaRepresentanteLegal { get; set; }

        [Display(Name = "Gerente")]
        public string Gerente { get; set; }

        [Display(Name = "Género del gerente")]
        [ForeignKey("GeneroGerente"), Column(Order = 1)]
        public int? GeneroGerenteId { get; set; }
        public virtual Genero GeneroGerente { get; set; }

        [Display(Name = "Región")]
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        [Display(Name = "Comuna")]
        public int? ComunaId { get; set; }
        public virtual Comuna Comuna { get; set; }

        [Display(Name = "Dirección Actual")]
        [DataType(DataType.MultilineText)]
        public string DireccionActual { get; set; }

        [Display(Name = "¿Hubo cambio de dirección?")]
        public bool CambioDireccion { get; set; }

        [Display(Name = "Fiscalizadores Contable")]
        public List<ActaFiscalizacionFiscalizadorContable> ActaFiscalizacionFiscalizadorContables { get; set; }

        [Display(Name = "Fiscalizadores legales")]
        public List<ActaFiscalizacionFiscalizadorLegal> ActaFiscalizacionFiscalizadorLegals { get; set; }

        [Display(Name = "Observaciones legales")]
        [DataType(DataType.MultilineText)]
        public string ObservacionLegal { get; set; }

        [Display(Name = "Observaciones contables")]
        [DataType(DataType.MultilineText)]
        public string ObservacionContable { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Workflow")]
        public int WorkflowId { get; set; }
        public virtual Workflow Workflow { get; set; }

        [Display(Name = "Hechos Contables verificados durante la Fiscalización")]
        public virtual List<ActaFiscalizacionHechoContable> ActaFiscalizacionHechoContables { get; set; }

        [Display(Name = "Hechos Legales verificados durante la Fiscalización")]
        public virtual List<ActaFiscalizacionHechoLegal> ActaFiscalizacionHechoLegals { get; set; }
    }
}