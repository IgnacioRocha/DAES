using System.Collections.Generic;
using DAES.Model.SistemaIntegrado;

namespace DAES.Model.DTO
{
    public class DTOBusquedaDocumento
    {
        public DTOBusquedaDocumento()
        {
            Documentos = new HashSet<Documento>();
        }

        public ICollection<Documento> Documentos { get; set; }
    }
}
