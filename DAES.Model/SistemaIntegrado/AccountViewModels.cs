using System.ComponentModel.DataAnnotations;

namespace DAES.Model.SistemaIntegrado
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        //public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "Es necesario especificar este dato")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Email notificación tareas")]
        public string EmailNotificacionTarea { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [StringLength(100, ErrorMessage = "La {0} debe contener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "Las password no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Perfil")]
        public int PerfilId { get; set; }
        public virtual Perfil Perfil { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Nombre funcionario")]
        public string Nombre { get; set; }

        [Display(Name = "Organización")]
        public string NombreOrganizacion { get; set; }

        public int? OrganizacionId { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
