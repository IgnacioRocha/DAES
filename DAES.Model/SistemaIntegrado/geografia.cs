using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("geografia")]
    public class geografia
    {
        public geografia()
        {
        }

        [Key]
        public int geografiaId { get; set; }
        public int regionid { get; set; }
        public int regioncodigo { get; set; }
        public string regionnombre { get; set; }
        public int provinciaid { get; set; }
        public int provinciacodigo { get; set; }
        public string provincianombre { get; set; }
        public int comunacodigo { get; set; }
        public string comunanombre { get; set; }
    }
}
