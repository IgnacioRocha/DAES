using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("Disolucion")]
    public class Disolucion
    {
        public Disolucion()
        {
            
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int DisolucionId { get; set; }

        [Display(Name = "Organización")]
        public int? OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        /*public int? ComisionLiquidadoraId { get; set; }
        public virtual ComisionLiquidadora ComisionLiquidadora { get; set; }*/

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
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaJuntaSocios { get; set; }

        [Display(Name = "¿Tiene Comision Liquidadora?")]
        public bool Comision { get; set; } = false;

        [Display(Name = "Fecha Disolución")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDisolucion { get; set; }

        #region Datos Cooperativa Anterior
        [Display(Name = "Tipo Documento")]
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
        public string NumeroFojas { get; set; }

        [Display(Name = "Año de inscripción")]
        public int? AñoInscripcion { get; set; }

        [Display(Name = "Datos Conservador Bienes Raices")]
        public string DatosCBR { get; set; }

        [Display(Name = "Datos Generales Notario Público y Notaría")]
        public string MinistroDeFe { get; set; }        

        #endregion

        #region Datos Asociaciones

        [Display(Name = "Numero de Oficio")]
        public int? NumeroOficio { get; set; }

        [Display(Name = "Fecha de Oficio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaOficio { get; set; }

        [Display(Name = "Fecha de Asamblea Extraordinario de Socios")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaAsambleaSocios { get; set; }

        [Display(Name = "Nombre de la Notaría")]
        public string NombreNotaria { get; set; }

        [Display(Name = "Datos del Notario")]
        public string DatosNotario { get; set; }

        /*[NotMapped]
        public string AñoDisolucion { get; set; }*/

        #endregion

        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPubliAnterior { get; set; }
        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPubliPosterior { get; set; }
        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaJuntaAnterior { get; set; }
        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaJuntaPosterior { get; set; }
        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDisAnterior { get; set; }
        [NotMapped]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDisPost { get; set; }

        public virtual List<ComisionLiquidadora> ComisionLiquidadoras { get; set; }

    }
}
