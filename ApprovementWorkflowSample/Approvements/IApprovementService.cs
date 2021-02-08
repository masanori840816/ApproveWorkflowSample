using System.Collections.Generic;
using System.Threading.Tasks;
namespace ApprovementWorkflowSample.Approvements
{
    public interface IApprovementService
    {
        Task<DisplayWorkflow> GetDisplayWorkflowAsync(int id);
        Task<List<WorkflowType>> GetAllWorkflowTypeAsync();
    }
}