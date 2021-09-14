using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.Supervision
{
    [Table("cacConcepto")]
    public partial class cacConcepto
    {
        public cacConcepto()
        {

        }

        [Key]
        public int concId { get; set; }
        public string concdescripcion { get; set; }
        public string concglosa { get; set; }
    }
}