using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAES.Model.DTO
{
    public class WorkflowDTO
    {
        public int WorkflowId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Asunto { get; set; }
        public string Definicion { get; set; }
        public bool TareaPersonal { get; set; }
        public string NombreFuncionario { get; set; }
        public string Pl_UndDes { get; set; }
        public string Grupo { get; set; }
        public string Mensaje { get; set; }

        public int ProcesoId { get; set; }
        public DateTime? ProcesoFechaVencimiento { get; set; }
        public string ProcesoDefinicion { get; set; }
        public string ProcesoNombreFuncionario { get; set; }
        public string ProcesoEmail { get; set; }
        public string ProcesoEntidad { get; set; }
        //public GD GD { get; set; }

        public bool EsPersonal { get; set; }
    }
}
