using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApprovementWorkflowSample.Applications;
using ApprovementWorkflowSample.Approvements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace ApprovementWorkflowSample.Views
{
    public partial class EditWorkflow
    {
        [Inject]
        public NavigationManager? NavigationManager { get; init; }
        [Inject]
        public IApprovementService? ApprovementService { get; init; }

        [Parameter]
        public int? WorkflowId { get; set; }
        [Parameter]
        public string WorkflowTitle { get; set; } = "";
        [Parameter]
        public List<WorkflowType> WorkflowTypes { get; set; } = new List<WorkflowType>(); 
        private DisplayWorkflow? currentWorkflow;
        protected override async Task OnInitializedAsync()
        {
            var query = new Uri(NavigationManager!.Uri).Query;
            
             if (QueryHelpers.ParseQuery(query).TryGetValue("workflowId", out var value) &&
                int.TryParse(value, out var workflowId))
            {
                currentWorkflow = await ApprovementService!.GetDisplayWorkflowAsync(workflowId);
            }
            else
            {
                currentWorkflow = new DisplayWorkflow(-1, "", null, null, new List<DisplayApproverGroup>());
            }
            WorkflowTitle = currentWorkflow.Title;
            WorkflowTypes = await ApprovementService!.GetAllWorkflowTypeAsync();
        }
    }
}