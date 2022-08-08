using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.Sigper
{
    [Table("PeDatLab")]
    public class PeDatLab
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RH_NumInte { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RH_ContCod { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RH_Correla { get; set; }

        public int? RhConCar { get; set; }

        [StringLength(5)] public string RhConGra { get; set; }

        //[Column(TypeName = "money")]
        //public decimal? RhConSueBas { get; set; }

        public DateTime? RhConIni { get; set; }

        //public DateTime? RhConFin { get; set; }

        //[StringLength(1)]
        //public string RhConVig { get; set; }

        //[StringLength(3)]
        //public string RhConUniIns { get; set; }

        public int? RhConUniCod { get; set; }

        //[StringLength(12)]
        //public string RhConPla { get; set; }

        [StringLength(6)] public string RhConEsc { get; set; }

        //public short? RhConNumHrs { get; set; }

        //[StringLength(1)]
        //public string RhConSadp { get; set; }

        //[StringLength(1)]
        //public string RhConPro { get; set; }

        //[StringLength(15)]
        //public string RhConDocNro { get; set; }

        //public DateTime? RhConDocFch { get; set; }

        //[StringLength(3)]
        //public string RhConDocTip { get; set; }

        //[StringLength(2)]
        //public string RhConHonTip { get; set; }

        //[StringLength(15)]
        //public string RhConRol { get; set; }

        //public short? RhConEmeCod { get; set; }

        //[StringLength(1)]
        //public string RhConOri { get; set; }

        //public short? RhConCorVac { get; set; }

        //public short? Rhconcfi { get; set; }

        //public DateTime? RhConFecTer { get; set; }

        //public short? RhConCorTer { get; set; }

        //public short? DgMotSupCod { get; set; }

        //public short? DgMotTerCod { get; set; }

        //[StringLength(11)]
        //public string RhConRGUCod { get; set; }

        //public short? RhConJor { get; set; }

        //public decimal? RhConPytCod { get; set; }

        //[StringLength(3)]
        //public string RhConSecInst { get; set; }

        //public int? RhConSecUnd { get; set; }

        //public short? RhConSecId { get; set; }

        //public short? RhCodBieL { get; set; }

        //public short? RhConTipHon { get; set; }

        //public int? PeDatLabCarFun { get; set; }

        //[StringLength(3)]
        //public string PeDatLabCpaIns { get; set; }

        //[StringLength(6)]
        //public string PeDatLabCpaCod { get; set; }

        //[StringLength(3)]
        //public string PeDatLabSedIns { get; set; }

        //public short? PeDatLabSed { get; set; }

        //public int? PeDatLabPdo { get; set; }

        //public short? PeDatLabMat { get; set; }

        public short? PeDatLabEst { get; set; }

        //public int? PeDatLabSes { get; set; }

        //[StringLength(300)]
        //public string PeDatLabGlo { get; set; }

        //public DateTime? PeDatLabCarFunIni { get; set; }

        //public DateTime? PeDatLabCarFunFin { get; set; }

        //public int? PeDatLabAdUndCod { get; set; }

        //[StringLength(3)]
        //public string PeDatLabEd_DocTip { get; set; }

        public int? PeDatLabAdDocCor { get; set; }

        //public short? PeDatLabAdDocNomCor { get; set; }

        //public int? PeDatLabCorHonRec { get; set; }

        //[StringLength(3)]
        //public string PeDatLabMotConIns { get; set; }

        //public short? PeDatLabMotConCod { get; set; }
    }
}