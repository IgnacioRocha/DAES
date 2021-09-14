using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("DocumentoOrganizacion")]
    public class DocumentoOrganizacion
    {
        public DocumentoOrganizacion()
        {
        }

        [Key]
        public int DocumentoOrganizacionId { get; set; }
        public int OrganizacionId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Autor { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public string Url { get; set; }
        public bool Importado { get; set; }

        public Guid? uniqueid { get; set; }
    }
}
