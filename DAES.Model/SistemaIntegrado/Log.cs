using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Log")]
    public class Log
    {
        [Display(Name = "Id")]
        public Guid LogId { get; set; }

        [Display(Name = "UserName")]
        public string LogUserName { get; set; }

        [Display(Name = "IP")]
        public string LogIpAddress { get; set; }

        [Display(Name = "Módulo")]
        public string LogAreaAccessed { get; set; }

        [Display(Name = "FechaCreacion/hora local")]
        public DateTime LogTimeLocal { get; set; }

        [Display(Name = "FechaCreacion/hora UTC")]
        public DateTime LogTimeUtc { get; set; }

        [Display(Name = "Detalles")]
        public string LogDetails { get; set; }

        [Display(Name = "Agente")]
        public string LogAgent { get; set; }

        [Display(Name = "Método HTTP")]
        public string LogHttpMethod { get; set; }

        [Display(Name = "Header")]
        public string LogHeader { get; set; }

        [Display(Name = "Contenido")]
        public string LogContent { get; set; }
    }
}
