using System;
using ApprovementWorkflowSample.Applications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace ApprovementWorkflowSample.Views
{
    public partial class EditWorkflow
    {
        [Inject]
        public SignInManager<ApplicationUser>? SignInManager { get; init; }
        [Inject]
        public UserManager<ApplicationUser>? UserManager { get; init; }

        public EditWorkflow()
        {
            
        }
    }
}