using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("DisolucionCooperativa")]
    public class DisolucionCooperativa
    {
        public DisolucionCooperativa()
        {
        }
        #region Datos Comunes de ambas cooperativas

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int DisolucionCooperativaId { get; set; }

        [Display(Name ="OrganizacionId")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name = "TipoOrganizacionId")]
        public int? TipoOrganizacionId { get; set; }
        public virtual TipoOrganizacion TipoOrganizacion { get; set; }

        [Display(Name = "Fecha Escritura Publica")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEscrituraPublica { get; set; }

        [Display(Name = "Fecha Publicacion Diario Oficial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPublicacionDiarioOficial { get; set; }

        [Display(Name = "Fecha Junta General de Socios")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaJuntaSocios { get; set; }

        [Display(Name = "Comision Liquidadora")]
        public bool ComisionLiquidadora { get; set; }

        [Display(Name = "Fecha Disolución")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDisolucion { get; set; }

        #endregion

        #region Datos Cooperativa Anterior
        [Display(Name = "Tipo Norma")]
        public int? TipoNormaId { get; set; }
        public virtual TipoNorma TipoNorma { get; set; }

        [Display(Name = "Número de Norma")]
        public int? NumeroNorma { get; set; }

        [Display(Name = "Fecha de Norma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNorma { get; set; }

        [Display(Name = "Autorizada por")]
        public string Autorizacion { get; set; }

        #endregion

        #region Datos Cooperativa Posterior

        [Display(Name = "Fojas; Número")]
        public int? NumeroFojas { get; set; }

        [Display(Name = "Año de inscripción")]
        public int? AñoInscripcion { get; set; }

        [Display(Name = "Datos Conserbador Bienes Raices")]
        public string DatosCBR { get; set; }

        [Display(Name = "Datos Generales Notario Público y Notaría")]
        public string MinistroDeFe { get; set; }

        #endregion
    }
}
