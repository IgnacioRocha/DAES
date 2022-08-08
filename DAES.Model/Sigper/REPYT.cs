using System.ComponentModel.DataAnnotations;

namespace DAES.Model.Sigper
{
    public class REPYT //programa
    {
        [Key]
        [Display(Name = "RePytCod")]
        public decimal RePytCod { get; set; }

        [Display(Name = "RePytDes")]
        public string RePytDes { get; set; }

        [Display(Name = "RePytEst")]
        public string RePytEst { get; set; }
    }
}
