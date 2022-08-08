using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.Sigper
{
    [Table("PEDATPER")]
    public class PEDATPER
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RH_NumInte { get; set; }

        [StringLength(1)]
        public string RH_DvNuInt { get; set; }

        [StringLength(1)]
        public string RH_EstLab { get; set; }

        [StringLength(80)]
        private string rh_Mail;
        public string Rh_Mail
        {
            get => string.IsNullOrEmpty(rh_Mail) ? null : rh_Mail.Trim().ToLower();
            set => rh_Mail = value;
        }

        [StringLength(80)]
        public string Rh_MailPer { get; set; }

        public int? RhSegUnd01 { get; set; }

        public int? RhSegUnd02 { get; set; }

        public int? RhSegUnd03 { get; set; }

        [StringLength(70)]
        public string RH_NomFunCap { get; set; }

        [StringLength(60)]
        public string PeDatPerChq { get; set; }

        [NotMapped]
        public PLUNILAB PLUNILAB { get; set; }
    }
}
