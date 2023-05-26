namespace DAES.Model.GestionDocumental
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProcesoDocumento")]
    public partial class ProcesoDocumento
    {
        public int Id { get; set; }

        public int IdFuncionario { get; set; }

        [Required]
        [StringLength(150)]
        public string Pdo_NombreFuncionario { get; set; }

        public int? Pdo_IdUnidadFuncionario { get; set; }

        [StringLength(150)]
        public string Pdo_NombreUnidadFuncionario { get; set; }

        public int Pdo_Secuencia { get; set; }

        public bool Pdo_VBSiguienteProceso { get; set; }

        public string Pdo_ObsSiguienteProceso { get; set; }

        public int? Pdo_IdUnidadEntidadDestino { get; set; }

        [StringLength(150)]
        public string Pdo_NombreUnidadEntidadDestino { get; set; }

        public int? Pdo_IdUsuarioDestino { get; set; }

        [StringLength(150)]
        public string Pdo_NombreUsuarioDestino { get; set; }

        public DateTime Pdo_FechaCreacion { get; set; }

        public DateTime? Pdo_FechaModificacion { get; set; }

        public bool? Pdo_Completado { get; set; }

        public bool Activo { get; set; }

        public int Documento_Id { get; set; }

        public int TipoDestino_Id { get; set; }

        public int TipoProceso_Id { get; set; }

        public virtual Documento Documento { get; set; }

        public virtual TipoDestino TipoDestino { get; set; }

        public virtual TipoProceso TipoProceso { get; set; }
    }
}
