using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApprovementWorkflowSample.Approvements
{
    public interface IWorkflows
    {
        Task<List<WorkflowType>> GetAllWorkflowTypeAsync();
    }
}