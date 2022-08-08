using System.Collections.Generic;

namespace DAES.BLL.Interfaces
{
    public interface IFolio
    {
        List<Model.FirmaDocumento.DTOTipoDocumento> GetTipoDocumento();
        Model.FirmaDocumento.DTOSolicitud GetFolio(string solicitante, string tipoDocumento, string subSecretaria);
    }
}