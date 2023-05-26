using System.ComponentModel.DataAnnotations;

namespace DAES.Model.Sigper
{
    public class LREMREP1Level1
    {
        [Key]
        [Display(Name = "lrem_codrep")]
        public int lrem_codrep { get; set; }

        [Display(Name = "lrem_tipo")]
        public int lrem_tipo { get; set; }
        
        [Display(Name = "lrem_reforcod")]
        public string lrem_reforcod { get; set; }
    }
}
