using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ApprovementWorkflowSample.Applications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ApprovementWorkflowSample.Views.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public NavigationManager? Navigation { get; init; }
        [Inject]
        public SignInManager<ApplicationUser>? SignInManager { get; init; }
        [Inject]
        public IHostEnvironmentAuthenticationStateProvider? HostAuthentication { get; init; }
        [CascadingParameter]
        private Task<AuthenticationState>? AuthenticationStateTask { get; init; }
        
        public MainLayout()
        {
            
        }
        public async Task SignOutAsync()
        {
            var authenticationState = await AuthenticationStateTask!;
            if (authenticationState?.User?.Identity is null ||
                    authenticationState.User.Identity.IsAuthenticated == false)
            {
                Navigation!.NavigateTo("Pages/SignIn", true);
                return;
            }
            HostAuthentication!.SetAuthenticationState(
                Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
            );
        }
    }
}