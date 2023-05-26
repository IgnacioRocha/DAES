using DAES.Model.DTO;
using System.Collections.Generic;

namespace DAES.BLL.Interfaces
{
    public interface IWorkflowService
    {
        List<WorkflowDTO> GetPendingTask(Model.Sigper.Sigper user);
    }
}