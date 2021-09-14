using DAES.Infrastructure.SistemaIntegrado;
using DAES.Model.SistemaIntegrado;
using DAES.Web.BackOffice.Helper;
using DAES.Web.BackOffice.Properties;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DAES.Web.BackOffice.Controllers {

    [Audit]
    [Authorize]
    public class AccountController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly SistemaIntegradoContext _db = new SistemaIntegradoContext();
        private BLL.Custom _custom = new BLL.Custom();

        public AccountController()
        {

        }

        public ActionResult Index()
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            var model = manager.Users.OrderBy(q => q.UserName).ToList();
            return View(model);
        }

        public ActionResult Details(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            var model = manager.Users.FirstOrDefault(q => q.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {

            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _db.Users.FirstOrDefault(q => q.Id == id);
            if (model == null) {
                return HttpNotFound();
            }

            ViewBag.PerfilId = new SelectList(_db.Perfil.OrderBy(q => q.Nombre), "PerfilId", "Nombre", model.PerfilId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser model)
        {

            if (model.PerfilId != (int)Infrastructure.Enum.Perfil.RepresentanteOrganizacion || string.IsNullOrWhiteSpace(model.NombreOrganizacion)) 
            {
                model.OrganizacionId = null;
            }

            if (model.PerfilId == (int)Infrastructure.Enum.Perfil.RepresentanteOrganizacion && !model.OrganizacionId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Debe especificar la organización");
            }

            if (ModelState.IsValid)
            {
                var user = _db.Users.FirstOrDefault(q => q.Id == model.Id);
                if (user != null)
                {
                    _db.Entry(user).State = EntityState.Modified;
                    user.Email = model.Email;
                    user.PerfilId = model.PerfilId;
                    user.OrganizacionId = model.OrganizacionId;
                    user.Habilitado = model.Habilitado;
                    _db.SaveChanges();

                    TempData["Message"] = Properties.Settings.Default.Success;
                    return RedirectToAction("Index");
                }
            }

            ViewBag.PerfilId = new SelectList(_db.Perfil.OrderBy(q => q.Nombre), "PerfilId", "Nombre", model.PerfilId);

            return View(model);
        }

        //public ActionResult Delete(string id) {
        //    if (id == null) {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var result = _custom.AccountDeleteValidate(id);
        //    foreach (var item in result) {
        //        ModelState.AddModelError(string.Empty, item);
        //    }

        //    var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
        //    var model = manager.Users.FirstOrDefault(q => q.Id == id);
        //    if (model == null) {
        //        return HttpNotFound();
        //    }
        //    return View(model);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id) {
        //    var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
        //    var user = manager.Users.FirstOrDefault(q => q.Id == id);

        //    if (_db.ActaFiscalizacionFiscalizadorContable.Any(q => q.UserId == id)) {
        //        _db.ActaFiscalizacionFiscalizadorContable.RemoveRange(_db.ActaFiscalizacionFiscalizadorContable.Where(q => q.UserId == id));
        //        _db.SaveChanges();
        //    }
        //    if (_db.ActaFiscalizacionFiscalizadorLegal.Any(q => q.UserId == id)) {
        //        _db.ActaFiscalizacionFiscalizadorLegal.RemoveRange(_db.ActaFiscalizacionFiscalizadorLegal.Where(q => q.UserId == id));
        //        _db.SaveChanges();
        //    }

        //    manager.Delete(user);

        //    TempData["Message"] = Settings.Default.Success;
        //    return RedirectToAction("Index");
        //}

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Acceso inválido.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.PerfilId = new SelectList(_db.Perfil.OrderBy(q => q.Nombre), "PerfilId", "Nombre");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (model.PerfilId == (int)Infrastructure.Enum.Perfil.RepresentanteOrganizacion && !model.OrganizacionId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Debe especificar la organización");
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, EmailNotificacionTarea = model.EmailNotificacionTarea, PerfilId = model.PerfilId, Nombre = model.Nombre, OrganizacionId = model.OrganizacionId };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    TempData["Message"] = Settings.Default.Success;
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            ViewBag.PerfilId = new SelectList(_db.Perfil.OrderBy(q => q.Nombre), "PerfilId", "Nombre");
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        //// GET: /Account/SendCode
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        //{
        //    var userId = await SignInManager.GetVerifiedUserIdAsync();
        //    if (userId == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });

                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        //[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        //protected override void Dispose(bool disposing) {
        //    if (disposing) {
        //        if (_userManager != null) {
        //            _userManager.Dispose();
        //            _userManager = null;
        //        }

        //        if (_signInManager != null) {
        //            _signInManager.Dispose();
        //            _signInManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {

            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion Helpers
    }
}