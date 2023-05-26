using DAES.Model.SistemaIntegrado;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAES.Model.DTO
{
    public class DTOConsultaProceso
    {
        public DTOConsultaProceso()
        {
            Procesos = new List<Proceso>();
            DefinicionProcesos = new List<DTODefinicionProceso>();
        }

        [Display(Name = "Texto de búsqueda")]
        public string Filter { get; set; }
        public List<Proceso> Procesos { get; set; }
        public List<DTODefinicionProceso> DefinicionProcesos { get; set; }
        [Display(Name = "Mostrar solo procesos en curso")]
        public bool MostrarSoloVigentes { get; set; }

        public class DTODefinicionProceso
        {
            public string text { get; set; }
            public int value { get; set; }
            public bool selected { get; set; }
        }

    }
}
