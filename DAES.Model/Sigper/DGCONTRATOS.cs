using System.ComponentModel.DataAnnotations;

namespace DAES.Model.Sigper
{
    public class DGCONTRATOS
    {
        [Key]
        [Display(Name = "RH_ContCod")]
        public short RH_ContCod { get; set; }

        [Display(Name = "RH_ContDes")]
        public string RH_ContDes { get; set; }
    }
}
