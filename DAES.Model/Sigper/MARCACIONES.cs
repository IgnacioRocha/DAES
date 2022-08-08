using System.ComponentModel.DataAnnotations;
using System;

namespace DAES.Model.Sigper
{
    public class MARCACIONES
    {
        [Display(Name = "IDENTIFICADOR")]
        public string IDENTIFICADOR { get; set; }

        //[Display(Name = "SERIAL")]
        //public string SERIAL { get; set; }

        [Display(Name = "FECHA")]
        public DateTime FECHA { get; set; }

        [Key]
        [Display(Name = "HORA")]
        public DateTime HORA { get; set; }

        [Display(Name = "IN_OUT")]
        public string IN_OUT { get; set; }
    }
}
