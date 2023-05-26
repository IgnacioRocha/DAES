using System;
using System.ComponentModel.DataAnnotations;

namespace DAES.Model.Sigper
{
    public class RePagHisDet
    {
        [Key]
        [Display(Name = "RH_NumInte")]
        public int RH_NumInte { get; set; }

        [Display(Name = "RehDetObjMon")]
        public Decimal? RehDetObjMon { get; set; }

        [Display(Name = "Re_Hismm")]
        public int Re_Hismm { get; set; }

        [Display(Name = "Re_Hisyy")]
        public int Re_Hisyy { get; set; }

        [Display(Name = "RehDetObj")]
        public string RehDetObj { get; set; }

        [Display(Name = "RehDetObjTip")]
        public string RehDetObjTip { get; set; }

    }
}
