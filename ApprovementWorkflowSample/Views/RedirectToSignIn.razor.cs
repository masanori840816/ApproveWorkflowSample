using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ApprovementWorkflowSample.Views
{
    public partial class RedirectToSignIn
    {
        [CascadingParameter]
        private Task<AuthenticationState>? AuthenticationStateTask { get; init; }
        [Inject]
        public NavigationManager? Navigation { get; init; }
        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await AuthenticationStateTask!;
            if (authenticationState?.User?.Identity is null ||
                    authenticationState.User.Identity.IsAuthenticated == false)
            {
                var returnUrl = Navigation!.ToBaseRelativePath(Navigation.Uri);
                if (string.IsNullOrWhiteSpace(returnUrl))
                {
                    Navigation.NavigateTo("Pages/SignIn", true);
                }
                else
                {
                    Navigation.NavigateTo($"Pages/SignIn?returnUrl={returnUrl}", true);
                }
            }
        }
    }
}