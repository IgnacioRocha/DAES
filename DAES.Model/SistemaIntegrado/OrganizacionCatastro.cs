using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.SistemaIntegrado
{
    [Table("OrganizacionCatastro")]
    public class OrganizacionCatastro
    {
        [Key]
        [Display(Name="Id")]
        public int IdCatastro { get; set; }

        [Display(Name ="Nombre del gerente")]
        public string NombreGerente { get; set; }
        [Display(Name = "Email gerente")]
        public string EmailGerente { get; set; }
        [Display(Name ="Rut del gerente")]
        public string RutGerente { get; set; }

        [Display(Name = "Nombre del segundo representante")]
        public string NombreRepresentante2 { get; set; }
        [Display(Name = "Email del segundo representante")]
        public string EmailRepresentante2 { get; set; }
        [Display(Name = "Rut del del segundo representante")]
        public string RutRepresentante2 { get; set; }

        [Display(Name = "Nombre del tercer representante")]
        public string NombreRepresentante3 { get; set; }
        [Display(Name = "Email del tercer representante")]
        public string EmailRepresentante3 { get; set; }
        [Display(Name = "Rut del del tercer representante")]
        public string RutRepresentante3 { get; set; }
    }
}
