using DAES.Web.FrontOffice.Helper;
using DAES.Web.FrontOffice.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Web.Mvc;

namespace DAES.Web.FrontOffice.Controllers
{
    [Audit]
    public class ClaveUnicaController : Controller
    {
        public ActionResult Index(string code, string state)
        {
            try
            {
                //validar datos de la sesión
                if (Global.CurrentClaveUnica == null)
                {
                    throw new Exception("Problema al conectar con clave única: No se ha iniciado el proceso de solicitud de clave única");
                }

                if (Global.CurrentClaveUnica.ClaveUnicaRequestAutorization == null)
                {
                    throw new Exception("Problema al conectar con clave única: No se ha iniciado el proceso de solicitud de autorización");
                }

                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new Exception("Problema al conectar con clave única: Code retornado es nulo");
                }

                if (string.IsNullOrWhiteSpace(state))
                {
                    throw new Exception("Problema al conectar con clave única: State retornado es nulo");
                }

                Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.state = state;
                Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.code = code;

                //paso 3, verificar si el token es válido
                if (!Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.IsValidToken)
                {
                    throw new Exception("Problema al conectar con clave única: El toke antifalsificación ya no es válido");
                }

                //paso 4, cambiar código de activacion de la sesion por los de respuesta
                Global.CurrentClaveUnica.ClaveUnicaTokenRequest = new ClaveUnicaTokenRequest();
                Global.CurrentClaveUnica.ClaveUnicaTokenRequest.code = Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.code;
                Global.CurrentClaveUnica.ClaveUnicaTokenRequest.state = Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.state;

                //5.  solicitar token de acceso para obtener info del ciudadano
                var clientToken = new RestClient(Global.CurrentClaveUnica.ClaveUnicaTokenRequest.url);
                var requestToken = new RestRequest(Method.POST);
                requestToken.AddHeader("content-type", "application/x-www-form-urlencoded");
                requestToken.AddParameter("application/x-www-form-urlencoded", Global.CurrentClaveUnica.ClaveUnicaTokenRequest.bodyRequest, ParameterType.RequestBody);

                IRestResponse responseToken = clientToken.Execute(requestToken);
                if (responseToken.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Problema al conectar con clave única: Error al solicitar token");
                }

                Global.CurrentClaveUnica.ClaveUnicaTokenResponse = new ClaveUnicaTokenResponse();
                Global.CurrentClaveUnica.ClaveUnicaTokenResponse = JsonConvert.DeserializeObject<ClaveUnicaTokenResponse>(responseToken.Content);

                //6. solicitar informacion del rut
                Global.CurrentClaveUnica.ClaveUnicaUserRequest = new ClaveUnicaUserRequest();

                //var clientUser = new RestClient("https://www.claveunica.gob.cl/openid/userinfo/");
                var clientUser = new RestClient("https://accounts.claveunica.gob.cl/openid/userinfo/");
                var requestUser = new RestRequest(Method.POST);
                requestUser.AddHeader("authorization", "Bearer " + Global.CurrentClaveUnica.ClaveUnicaTokenResponse.access_token);

                IRestResponse responseUser = clientUser.Execute(requestUser);
                if (responseUser.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Problema al conectar con clave única: Error al solicitar datos del usuario");
                }

                Global.CurrentClaveUnica.ClaveUnicaUser = new ClaveUnicaUser();
                Global.CurrentClaveUnica.ClaveUnicaUser = JsonConvert.DeserializeObject<ClaveUnicaUser>(responseUser.Content);

                TempData["Message"] = "Autenticación existosa con clave única.";

                if (string.IsNullOrWhiteSpace(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method) || string.IsNullOrWhiteSpace(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller))
                {
                    throw new Exception("No se especificó la página de retorno");
                }

                return RedirectToAction(Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.method, Global.CurrentClaveUnica.ClaveUnicaRequestAutorization.controller);
            }
            catch (Exception ex)
            {
                return View("_Error", ex);
            }
        }
    }
}