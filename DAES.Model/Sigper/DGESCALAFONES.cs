using System.ComponentModel.DataAnnotations;

namespace DAES.Model.Sigper
{
    public class DGESCALAFONES
    {
        [Key]
        [Display(Name = "Pl_CodEsc")]
        public string Pl_CodEsc { get; set; }

        [Display(Name = "Pl_DesEsc")]
        public string Pl_DesEsc { get; set; }
    }
}
