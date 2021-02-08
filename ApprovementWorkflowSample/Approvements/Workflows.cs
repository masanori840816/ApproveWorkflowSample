using System.Collections.Generic;
using System.Threading.Tasks;
using ApprovementWorkflowSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApprovementWorkflowSample.Approvements
{
    public class Workflows: IWorkflows
    {
        private readonly ILogger<Workflows> logger;
        private readonly ApprovementWorkflowContext context;
        public Workflows(ILogger<Workflows> logger,
                ApprovementWorkflowContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task<List<WorkflowType>> GetAllWorkflowTypeAsync()
        {
            return await context.WorkflowTypes.ToListAsync();
        }
    }
}