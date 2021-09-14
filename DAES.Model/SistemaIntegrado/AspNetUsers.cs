using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model
{
    [Table("AspNetUsers")]
    public class AspNetUsers
    {
        public AspNetUsers()
        {
        }

        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}