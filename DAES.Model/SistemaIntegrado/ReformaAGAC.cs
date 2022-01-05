using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.SistemaIntegrado
{
    [Table("ReformaAGAC")]
    public class ReformaAGAC
    {



        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int IdReformaAGAC { get; set; }

        [Display(Name = "Asamblea/Deposito")]
        public int? AsambleaDepId { get; set; }
        public virtual AsambleaDeposito AsambleaDeposito { get; set; }

        [Display(Name = "Fecha Asamblea/depósito")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaAsambleaDep { get; set; }

        [Display(Name = "Fecha de Oficio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaOficio { get; set; }

        [Display(Name = "Número de Oficio")]
        public int? NumeroOficio { get; set; }

        public virtual Aprobacion Aprobacion { get; set; }

        [Display(Name = "¿Aprobacion?")]
        public int? AprobacionId { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

    }
}
