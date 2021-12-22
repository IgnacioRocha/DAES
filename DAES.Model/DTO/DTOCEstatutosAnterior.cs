using DAES.Model.SistemaIntegrado;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.DTO
{
    public class DTOCEstatutosAnterior
    {
        public DTOCEstatutosAnterior()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int OrganizacionId { get; set; }

        //Norma
        public int TipoNorma { get; set; }

        [Display(Name = "Tipo norma")]
        public int TipoNormaId { get; set; }

        public virtual TipoNorma Tiponorma { get; set; }

        public int NumNorma { get; set; }

        [Display(Name = "Fecha norma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNorma { get; set; }

        [Display(Name = "Fecha publicacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPubli { get; set; }

        public string Autorizado { get; set; }

        //Saneamiento
        [Display(Name = "Fecha escritura publicacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEscriturapublica { get; set; }

        [Display(Name = "Fecha publicacion (Diario oficial)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDiarioOficial { get; set; }

        [Display(Name = "Datos generales notario público y notaría")]
        public string Datosnotario { get; set; }

        [Display(Name = "Fojas; Número")]
        public int Fojas { get; set; }

        [Display(Name = "Año inscripción")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AnoInscripcion { get; set; }

        [Display(Name = "Datos CBR")]
        public string Cbr { get; set; }

        //Reforma

        [Display(Name = "Fecha reforma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fechaReforma { get; set; }

        public bool lastReforma { get; set; }

        [Display(Name = "Número reforma")]
        public int NumeroReforma { get; set; }

        [Display(Name = "Fecha norma")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fechaNormaReforma { get; set; }

        [Display(Name = "Fecha publicacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fechaPubliReforma { get; set; }

        [Display(Name = "Datos generales notario público y notaría")]
        public string DatosNotarioReforma { get; set; }

        [Display(Name = "Tipo organización")]
        public int TipoOrganizacionId { get; set; }
        public virtual TipoOrganizacion TipoOrganizacion { get; set; }


        
        


    }

}
