using DAES.Model.Sigper;
using System.Collections.Generic;

namespace DAES.Infrastructure.Interfaces
{
    public interface ISIGPER
    {
        PLUNILAB GetUnidad(int codigo);
        SIGPER GetUserByRut(int rut);
        List<PEDATPER> GetUserByTerm(string term);
        List<PEDATPER> GetUserByTermUnidad(string term);
        List<PEDATPER> GetUserByUnidad(int codigo);
        List<PEDATPER> GetUserByUnidadWithoutHonorarios(int codigoUnidad);
        List<PEDATPER> GetAllUsersForCometido();
        List<PEDATPER> GetAllUsers();
        List<PLUNILAB> GetUnidades();
        SIGPER GetSecretariaByUnidad(int codigo);
        List<DGREGIONES> GetRegion();
        string GetRegionbyComuna(string codComuna);
        List<DGCOMUNAS> GetComunasbyRegion(string idRegion);
        List<DGCOMUNAS> GetDGCOMUNAs();
        List<DGESCALAFONES> GetGESCALAFONEs();
        List<PECARGOS> GetPECARGOs();
        List<DGESTAMENTOS> GetDGESTAMENTOs();
        List<ReContra> GetReContra();
        List<REPYT> GetREPYTs();
        List<DGCONTRATOS> GetDGCONTRATOS();
        List<PLUNILAB> GetUnidadesFirmantes(List<string> listEmailFirmantes);
        List<PEDATPER> GetUserFirmanteByUnidad(int codigoUnidad, List<string> listEmailFirmantes);
        SIGPER GetUserByEmail(string email);
        int GetBaseCalculoHorasExtras(int rut, int mes, int anno, int calidad);
        int GetHorasTrabajadas(string rut, int mes, int anno);
    }
}