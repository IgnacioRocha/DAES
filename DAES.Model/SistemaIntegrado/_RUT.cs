using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("_RUT")]
    public class _RUT
    {
        public _RUT()
        {
        }

        public int Id { get; set; }
        public string RUT { get; set; }
    }
}