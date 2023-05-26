using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.Supervision
{
    [Table("cacArea")]
    public partial class cacArea
    {
        public cacArea()
        {
            //cacFactors = new HashSet<cacFactor>();
        }

        [Key]
        public int areaId { get; set; }
        public int? areaSecuencia { get; set; }
        public string areaDescripcion { get; set; }
        public System.Decimal? areaPonderacion { get; set; }
        public virtual List<cacFactor> cacFactors { get; set; }

        public int? formid { get; set; }
        public virtual cacFormulario cacFormulario { get; set; }

    }
}