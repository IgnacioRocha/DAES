using DAES.Infrastructure;
using System;
using System.Collections.Generic;
using System.Web;

namespace DAES.Web.FrontOffice.Models
{
    public class ClaveUnica
    {
        public ClaveUnica()
        {
        }

        public ClaveUnicaRequestAutorization ClaveUnicaRequestAutorization { get; set; }
        public ClaveUnicaTokenRequest ClaveUnicaTokenRequest { get; set; }
        public ClaveUnicaTokenResponse ClaveUnicaTokenResponse { get; set; }
        public ClaveUnicaUserRequest ClaveUnicaUserRequest { get; set; }
        public ClaveUnicaUser ClaveUnicaUser { get; set; }

        public bool IsAutenticated
        {
            get
            {
                return this != null && ClaveUnicaRequestAutorization != null && ClaveUnicaUser != null;
            }
        }

        public string User
        {
            get
            {
                if (IsAutenticated && ClaveUnicaUser != null)
                    return string.Join(" ", ClaveUnicaUser.name.nombres).ToUpperNull() + " " + string.Join(" ", ClaveUnicaUser.name.apellidos).ToUpperNull();

                return null;
            }
        }
        public string RUT
        {
            get
            {
                if (IsAutenticated && ClaveUnicaUser != null)
                    return ClaveUnicaUser.RolUnico.numero.ToString();

                return null;
            }
        }
    }

    public class ClaveUnicaRequestAutorization
    {
        public ClaveUnicaRequestAutorization()
        {
        }

        public string client_id { get; set; } = Properties.Settings.Default.client_id;
        public string response_type { get; set; } = "code";
        public string scope { get; set; } = "openid run name";
        public string token { get; set; } = Guid.NewGuid().ToString("N");
        public string redirect_uri { get; set; } = Properties.Settings.Default.redirect_uri;
        public string state { get; set; }
        public string code { get; set; }
        public string controller { get; set; }
        public string method { get; set; }
        public string tramite { get; set; }
        public string tempUrl { get; set; }

        public string uri
        {
            get
            {
                return string.Concat(
                    "https://accounts.claveunica.gob.cl/openid/authorize?client_id=", client_id,
                    "&redirect_uri=", redirect_uri,
                    "&response_type=", response_type,
                    "&scope=", scope,
                    "&state=", token);
            }
        }



        public bool IsValidToken
        {
            get
            {
                return token == state;
            }
        }
    }
    public class ClaveUnicaTokenRequest
    {
        public ClaveUnicaTokenRequest()
        {
        }

        public string url { get; set; } = "https://accounts.claveunica.gob.cl/openid/token/";
        public string client_id { get; set; } = Properties.Settings.Default.client_id;
        public string client_secret { get; set; } = Properties.Settings.Default.client_secret;
        public string redirect_uri { get; set; } = Properties.Settings.Default.redirect_uri;
        public string grant_type { get; set; } = "authorization_code";
        public string code { get; set; }
        public string state { get; set; }

        public string bodyRequest
        {
            get
            {
                return string.Concat(
                    "client_id=", client_id,
                    "&client_secret=", client_secret,
                    "&redirect_uri=", Uri.EscapeDataString(Properties.Settings.Default.redirect_uri),
                    "&grant_type=", grant_type,
                    "&code=", code,
                    "&state=", state);
            }
        }


    }
    public class ClaveUnicaTokenResponse
    {
        public ClaveUnicaTokenResponse()
        {
        }

        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string id_token { get; set; }
    }
    public class ClaveUnicaUserRequest
    {
        public ClaveUnicaUserRequest()
        {
        }

        public string url { get; set; } = "https://www.claveunica.gob.cl/openid/userinfo/";
        public string authorization { get; set; }
    }
    public class RolUnico
    {
        public RolUnico()
        {

        }

        public int numero { get; set; }
        public string DV { get; set; }
        public string tipo { get; set; }
    }
    public class Name
    {
        public Name()
        {

        }
        public List<string> nombres { get; set; }
        public List<string> apellidos { get; set; }
    }
    public class ClaveUnicaUser
    {
        public ClaveUnicaUser()
        {
        }

        public string sub { get; set; }
        public RolUnico RolUnico { get; set; }
        public Name name { get; set; }
    }

    public static class Global
    {
        public static ClaveUnica CurrentClaveUnica
        {
            get
            {
                return HttpContext.Current.Session["ClaveUnica"] as ClaveUnica;
            }
            set
            {
                HttpContext.Current.Session["ClaveUnica"] = value;
            }
        }
    }
}