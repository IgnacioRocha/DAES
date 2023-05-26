using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Perfil")]
        public int PerfilId { get; set; }
        public virtual Perfil Perfil { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [NotMapped]
        [Display(Name = "Organización")]
        public string NombreOrganizacion { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Es necesario especificar este dato")]
        [Display(Name = "Email notificación tareas")]
        public string EmailNotificacionTarea { get; set; }

        [NotMapped]
        public bool Selected { get; set; }
        public virtual ICollection<Workflow> Workflows { get; set; }

        public bool? EsFiscalizador { get; set; }

        public bool Habilitado { get; set; }
    }
}