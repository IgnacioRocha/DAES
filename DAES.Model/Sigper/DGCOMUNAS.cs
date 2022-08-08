using System.ComponentModel.DataAnnotations;

namespace DAES.Model.Sigper
{
    public class DGCOMUNAS
    {
        //[Key]
        [Display(Name = "Pl_CodReg")]
        public string Pl_CodReg { get; set; }

        [Key]
        [Display(Name = "Pl_CodCom")]
        public string Pl_CodCom { get; set; }

        [Display(Name = "Pl_DesCom")]
        public string Pl_DesCom { get; set; }
    }
}
