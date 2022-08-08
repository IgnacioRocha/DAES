using DAES.BLL.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Infrastructure.Folio
{
    public class Folio : IFolio
    {
        string url = "http://wsfolio.economia.cl";

        public List<Model.FirmaDocumento.DTOTipoDocumento> GetTipoDocumento()
        {
            var client = new RestClient(url + "/api/tipodocumento");
            var response = client.Execute(new RestRequest());
            var returnValue = JsonConvert.DeserializeObject<List<Model.FirmaDocumento.DTOTipoDocumento>>(response.Content);
            return returnValue;
        }

        public Model.FirmaDocumento.DTOSolicitud GetFolio(string solicitante, string tipoDocumento, string subSecretaria)
        {
            var solicitud = new Model.FirmaDocumento.DTOSolicitud()
            {
                periodo = DateTime.Now.Year.ToString(),
                solicitante = solicitante,
                tipodocumento = tipoDocumento,
                subsecretaria = subSecretaria
            };

            var client = new RestClient(url + "/api/folio");
            var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

            request.AddJsonBody(solicitud);
            IRestResponse response = client.Execute(request);
            var returnValue = JsonConvert.DeserializeObject<Model.FirmaDocumento.DTOSolicitud>(response.Content);
            return returnValue;
        }
    }
}
