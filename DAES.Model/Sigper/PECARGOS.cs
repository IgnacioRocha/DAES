using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.Sigper
{
    [Table("PECARGOS")]
    public class PECARGOS
    {
        [Key]
        public int Pl_CodCar  { get; set; }

        public string Pl_DesCar { get; set; }

    }
}
