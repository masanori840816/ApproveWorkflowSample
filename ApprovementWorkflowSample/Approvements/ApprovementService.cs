using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ApprovementWorkflowSample.Approvements
{
    public class ApprovementService: IApprovementService
    {
        private readonly ILogger<ApprovementService> logger;
        private readonly IWorkflows workflows;
        public ApprovementService(ILogger<ApprovementService> logger,
                IWorkflows workflows)
        {
            this.logger = logger;
            this.workflows = workflows;
        }
        public async Task<DisplayWorkflow> GetDisplayWorkflowAsync(int id)
        {
            // TODO: implement
            return new DisplayWorkflow(-1, "", null, null, new List<DisplayApproverGroup>());
        }
        public async Task<List<WorkflowType>> GetAllWorkflowTypeAsync()
        {
            return await workflows.GetAllWorkflowTypeAsync();
        }
    }
}