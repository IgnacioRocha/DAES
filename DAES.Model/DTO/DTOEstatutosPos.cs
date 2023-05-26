using System;
using System.ComponentModel.DataAnnotations;

namespace DAES.Model.DTO
{
    class DTOEstatutosPos
    {
        public DTOEstatutosPos()
        {

        }

        //Norma
        [Display(Name = "Fecha junta general constitutiva de socios")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaJuntaSocios { get; set; }

        [Display(Name = "Fecha escritura publica")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEscrituraPublica { get; set; }

        [Display(Name = "Fecha de publicación(Diario oficial)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPublicacionDiario { get; set; }

        [Display(Name = "Datos generales notario público y notaría")]
        public string DatosNotarioPublico { get; set; }

        [Display(Name = "Fojas; Número")]
        public int FojasNumero { get; set; }

        [Display(Name = "año inscripción")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? anoInscripcion { get; set; }

        [Display(Name = "Datos CBR")]
        public string DatosCBR { get; set; }

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
    }
}
