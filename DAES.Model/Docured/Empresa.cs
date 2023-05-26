namespace DAES.Model.Docured
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Empresa")]
    public partial class Empresa
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int codigo_empresa { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string nombre { get; set; }

        [StringLength(50)]
        public string direccion { get; set; }

        [StringLength(20)]
        public string telefono { get; set; }

        [StringLength(20)]
        public string comuna { get; set; }

        [StringLength(20)]
        public string ciudad { get; set; }

        [StringLength(20)]
        public string email { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string Path_Imagenes { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string Path_Internet { get; set; }

        [StringLength(255)]
        public string Servidor_Web { get; set; }

        [StringLength(255)]
        public string Servidor_SMTP { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Tipo_Correo { get; set; }

        [StringLength(255)]
        public string Servidor_FTP { get; set; }

        [StringLength(10)]
        public string Puerto_FTP { get; set; }

        [StringLength(50)]
        public string Usuario_FTP { get; set; }

        [StringLength(50)]
        public string Clave_FTP { get; set; }

        public int? Tipo_FileTransfer { get; set; }

        [StringLength(10)]
        public string Puerto_SMTP { get; set; }

        public int? Habilitar_Der_Doc { get; set; }
    }
}
