using System;
using System.ComponentModel.DataAnnotations;

namespace DAES.Model.DTO
{
    public class DTODocumentoGD
    {
        public DTODocumentoGD()
        {
        }

        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Correlativo")]
        public string Doc_Correlativo { get; set; }
        [Display(Name = "Creación")]
        public DateTime Doc_FechaCreacion { get; set; }
        [Display(Name = "Tipo")]
        public string Tdo_Nombre { get; set; }
        [Display(Name = "Asunto")]
        public string Doc_Asunto { get; set; }
        [Display(Name = "Referencia")]
        public string Doc_Referencia { get; set; }
        [Display(Name = "Descripción")]
        public string Doc_Descripcion { get; set; }
        [Display(Name = "Con procesos?")]
        public bool ConProcesos { get; set; }
    }
}
