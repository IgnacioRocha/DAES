using System;
using System.ComponentModel.DataAnnotations;

namespace DAES.Model.Sigper
{
    public class ReContra
    {
        [Key]
        [Display(Name = "RH_NumInte")]
        public int RH_NumInte { get; set; }

        [Display(Name = "Re_ConPyt")]
        public Decimal Re_ConPyt { get; set; }

        [Display(Name = "ReContraSed")]
        public Int16 ReContraSed { get; set; }

        [Display(Name = "Re_ConIni")]
        public DateTime Re_ConIni { get; set; }

        [Display(Name = "Re_ConPyt")]
        public Decimal Re_SuelBas { get; set; }

        [Display(Name = "Re_ConCar")]
        public Int32 Re_ConCar { get; set; }

        [Display(Name = "RE_ConCor")]
        public Int32 RE_ConCor { get; set; }

        [Display(Name = "RH_ContCod")]
        public Int16 RH_ContCod { get; set; }

        [Display(Name = "ReContraLabCor")]
        public int ReContraLabCor { get; set; }

        [Display(Name = "Re_ConUni")]
        public Int32 Re_ConUni { get; set; }

        [Display(Name = "Re_ConEsc")]
        public string Re_ConEsc { get; set; }

        [Display(Name = "ReContraEst")]
        public Int16 ReContraEst { get; set; }

        [Display(Name = "Re_ConGra")]
        public string Re_ConGra { get; set; }
    }
}
