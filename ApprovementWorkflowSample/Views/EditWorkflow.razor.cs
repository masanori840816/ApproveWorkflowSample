using System;
using System.Threading.Tasks;
using ApprovementWorkflowSample.Applications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace ApprovementWorkflowSample.Views
{
    public partial class EditWorkflow
    {
        [Inject]
        public SignInManager<ApplicationUser>? SignInManager { get; init; }
        [Inject]
        public UserManager<ApplicationUser>? UserManager { get; init; }
        [Inject]
        public NavigationManager? NavigationManager { get; init; }

        protected override async Task OnInitializedAsync()
        {
            var query = new Uri(NavigationManager!.Uri).Query;
            
             if (QueryHelpers.ParseQuery(query).TryGetValue("sampleId", out var value))
            {
                Console.WriteLine(value);
            }
        }
    }
}