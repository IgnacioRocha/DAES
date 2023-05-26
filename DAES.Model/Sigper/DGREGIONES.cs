using System.ComponentModel.DataAnnotations;

namespace DAES.Model.Sigper
{
    public class DGREGIONES
    {
        [Key]
        [Display(Name = "Pl_CodReg")]
        public string Pl_CodReg { get; set; }

        [Display(Name = "Pl_DesReg")]
        public string Pl_DesReg { get; set; }
    }
}
