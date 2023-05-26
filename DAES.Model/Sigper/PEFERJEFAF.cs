
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.Sigper
{

    [Table("PEFERJEFAF")]
    public class PEFERJEFAF
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string PeFerJerInst { get; set; }
        public decimal PeFerJerCod { get; set; }
        public int FyPFunRut { get; set; }
    }
}
