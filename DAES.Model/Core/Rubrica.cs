using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.Core
{
    [Table("CoreRubrica")]
    public class Rubrica
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int RubricaId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido")]
        [Display(Name = "Correo electrónico usuario")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Identificador de firma electrónica")]
        public string IdentificadorFirma { get; set; }

        [Display(Name = "Unidad organizacional (opcional)")]
        public string UnidadOrganizacional { get; set; }

        [Display(Name = "Habilitado para firmar")]
        public bool HabilitadoFirma { get; set; }
    }
}
