using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("TipoPersonaJuridica")]
    public class TipoPersonaJuridica
    {
        public TipoPersonaJuridica()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int TipoPersonaJuridicaId { get; set; }

        public string NombrePersonaJuridica { get; set; }
    }
}
