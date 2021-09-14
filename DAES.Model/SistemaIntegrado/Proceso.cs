using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Proceso")]
    public class Proceso
    {
        public Proceso()
        {
            Workflows = new HashSet<Workflow>();
            Documentos = new HashSet<Documento>();
            ActualizacionOrganizacions = new HashSet<ActualizacionOrganizacion>();
            Articulo91s = new HashSet<Articulo91>();
            Fiscalizacions = new HashSet<Fiscalizacion>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Editable(false)]
        [Display(Name = "Id")]
        public int ProcesoId { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Definición proceso")]
        public int DefinicionProcesoId { get; set; }
        public virtual DefinicionProceso DefinicionProceso { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha creación")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha vencimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaVencimiento { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha término")]
        [DataType(DataType.Date)]
        public DateTime? FechaTermino { get; set; }

        [Display(Name = "Creador")]
        public string Creador { get; set; }

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }

        [Display(Name = "Terminado?")]
        public bool Terminada { get; set; }

        [Display(Name = "Id documento GD")]
        public int DocumentoId { get; set; }

        [Display(Name = "Correlativo GD")]
        public string Correlativo { get; set; }

        [Display(Name = "Organización")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "Solicitante")]
        public int? SolicitanteId { get; set; }
        public virtual Solicitante Solicitante { get; set; }

        [NotMapped]
        public string UserId { get; set; }

        public virtual ICollection<Workflow> Workflows { get; set; }
        public virtual ICollection<ActualizacionOrganizacion> ActualizacionOrganizacions { get; set; }
        public virtual ICollection<Documento> Documentos { get; set; }
        public virtual ICollection<Articulo91> Articulo91s { get; set; }

        [Display(Name = "Fiscalizaciones")]
        public virtual ICollection<Fiscalizacion> Fiscalizacions { get; set; }
    }
}