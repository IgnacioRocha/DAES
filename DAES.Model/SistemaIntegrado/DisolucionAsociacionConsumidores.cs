using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.Model.SistemaIntegrado
{
    [Table("DisolucionAsociacionConsumidores")]
    public class DisolucionAsociacionConsumidores
    {
        public DisolucionAsociacionConsumidores()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int DisolucionAsociacionConsumidoresId { get; set; }

        [Display(Name ="Organizacion")]
        public int OrganizacionId { get; set; }
        public virtual Organizacion Organizacion { get; set; }

        [Display(Name ="Numero de Oficio")]
        public int NumeroOficio { get; set; }

        public int TipoOrganizacionId { get; set; }
        public virtual TipoOrganizacion TipoOrganizacion { get; set; }

        [Display(Name ="Fecha de Oficio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaOficio { get; set; }

        [Display(Name ="Fecha de Asamble Extraordinario de Socios")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaAsambleaSocios { get; set; }

        [Display(Name ="Fecha Escritura Publica")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaEscrituraPublica { get; set; }
        
        [Display(Name = "Fecha de Publicación en el Diario Oficial")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaPublicacionDiarioOficial { get; set; }
        
        [Display(Name = "Nombre de la Notaría")]
        public string NombreNotaria { get; set; }

        [Display(Name = "Datos del Notario")]
        public string DatosNotario { get; set; }
    }
}
