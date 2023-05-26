using DAES.BLL;
using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Helper
{

    public class AuditAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext filterContext) {

            var request = filterContext.HttpContext.Request;

            var headers = string.Empty;
            foreach (var key in request.Headers.AllKeys) {
                if (key != null) {
                    headers += key + " = " + request.Headers[key] + Environment.NewLine;
                }
            }

            var content = string.Empty;
            if (request.Files.Count == 0) {
                var parsed = HttpUtility.ParseQueryString(Encoding.Default.GetString(request.BinaryRead(request.TotalBytes)));
                foreach (var key in parsed.AllKeys) {
                    if (!string.IsNullOrEmpty(key)) {
                        if (!key.ToUpper().Contains("PASSWORD")) {
                            content += key + " = " + parsed[key] + Environment.NewLine;
                        }
                    }
                }
            }

            var log = new Log {
                LogId = Guid.NewGuid(),
                LogUserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                LogIpAddress = request.UserHostAddress ?? request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.ServerVariables["REMOTE_ADDR"],
                LogAreaAccessed = request.RawUrl,
                LogTimeUtc = DateTime.UtcNow,
                LogTimeLocal = DateTime.Now,
                LogAgent = request.UserAgent,
                LogHttpMethod = request.HttpMethod,
                LogHeader = headers,
                LogContent = content
            };

            Custom bll = new Custom();
            bll.LogAdd(log);
            base.OnActionExecuting(filterContext);
        }
    }
    public static class Helper {
        public static ApplicationUser CurrentUser {
            get {
                var db = new SistemaIntegradoContext();
                return db.Users.FirstOrDefault(q => q.UserName == HttpContext.Current.User.Identity.Name);
            }
        }
    }
}