namespace DAES.Model.GestionDocumental
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Adjunto")]
    public partial class Adjunto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string IdTabla { get; set; }

        public int IdRegistro { get; set; }

        [Required]
        [StringLength(50)]
        public string Adj_IdSharePoint { get; set; }

        [Required]
        [StringLength(255)]
        public string Adj_Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string Adj_TipoMime { get; set; }

        public byte[] Adj_Contenido { get; set; }

        [StringLength(100)]
        public string Adj_Numero { get; set; }

        public int? Adj_TotalPaginas { get; set; }

        public int? Adj_TotalPaginasSubidas { get; set; }

        public string Adj_Descripcion { get; set; }

        [StringLength(100)]
        public string Adj_LoginUsuario { get; set; }

        public DateTime Adj_FechaCreacion { get; set; }

        public DateTime? Adj_FechaModificacion { get; set; }

        public bool Activo { get; set; }

        public int? TipoAdjunto_Id { get; set; }

        [StringLength(500)]
        public string Adj_Url { get; set; }

        public virtual TipoAdjunto TipoAdjunto { get; set; }
    }
}
