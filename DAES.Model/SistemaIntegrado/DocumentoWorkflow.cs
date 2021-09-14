using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("DocumentoWorkflow")]
    public class DocumentoWorkflow
    {
        public DocumentoWorkflow()
        {
        }

        [Key]
        public int DocumentoWorkflowId { get; set; }
        public int WorkflowId { get; set; }
        public virtual Workflow Workflow { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Autor { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public string Url { get; set; }
        public DateTime? FechaRespuesta { get; set; } = null;
        public string Recordatorio { get; set; }
        public bool Resuelto { get; set; } = false;
        public bool Importado { get; set; }
        public Guid? uniqueid { get; set; }

    }
}
