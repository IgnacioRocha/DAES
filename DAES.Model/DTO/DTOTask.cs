using DAES.Model.SistemaIntegrado;
using System.Collections.Generic;

namespace DAES.Model.DTO
{
    public class DTOTask
    {
        public DTOTask()
        {
            Workflows = new HashSet<Workflow>();
        }

        public ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Workflow> Workflows { get; set; }
    }
}
