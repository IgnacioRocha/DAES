﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("EstudioSocioEconomico")]
    public class EstudioSocioEconomico
    {
        public EstudioSocioEconomico()
        {
            
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int IdEstudio { get; set; }

        [Required(ErrorMessage = "Es necesario adjuntar un documento")]
        [Display(Name = "Documento")]
        [DataType(DataType.Upload)]
        public byte[] DocumentoAdjunto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaCreacion { get; set; }

        public int ProcesoId { get; set; }
        public virtual Proceso Proceso { get; set; }



    }
}

