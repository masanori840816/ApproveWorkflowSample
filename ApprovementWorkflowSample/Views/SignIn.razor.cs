using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ApprovementWorkflowSample.Applications;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ApprovementWorkflowSample.Views
{
    public partial class SignIn
    {
        [Inject]
        public IJSRuntime? JSRuntime { get; init; }
        [Inject]
        public NavigationManager? Navigation { get; init; }
        [Inject]
        public IApplicationUserService? ApplicationUsers{get; init; }
        [Inject]
        public SignInManager<ApplicationUser>? SignInManager { get; init; }
        [Inject]
        public IHostEnvironmentAuthenticationStateProvider? HostAuthentication { get; init; }
        [Inject]
        public AuthenticationStateProvider? AuthenticationStateProvider{get; init; }

        [Parameter]
        public string Email { get; set; } = "";
        [Parameter]
        public string Password { get; set; } = "";
        [Parameter]
        public string AdditionalClassName { get; set; } = "";
        public async Task StartSigningIn()
        {
            if(string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Password))
            {
                await HandleSigningInFailedAsync("Email and Password are required");
                return;
            }
            ApplicationUser? user = await ApplicationUsers!.GetUserByEmailAsync(Email);
            if(user == null)
            {
                await HandleSigningInFailedAsync("Email or Password are not match");
                return;
            }
            SignInResult loginResult = await SignInManager!.CheckPasswordSignInAsync(user, Password, false);
            if(loginResult.Succeeded == false)
            {
                await HandleSigningInFailedAsync("Email or Password are not match");
                return;
            }
            if(loginResult.Succeeded)
            {
                ClaimsPrincipal principal = await SignInManager.CreateUserPrincipalAsync(user);
                ClaimsIdentity identity = new ClaimsIdentity(principal.Claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);
                SignInManager.Context.User = principal;
                HostAuthentication!.SetAuthenticationState(
                    Task.FromResult(new AuthenticationState(principal)));

                AuthenticationState authState = await AuthenticationStateProvider!.GetAuthenticationStateAsync();
            
                Navigation!.NavigateTo("/Pages/Edit");
            }
        }
        private async Task HandleSigningInFailedAsync(string errorMessage)
        {
            AdditionalClassName = "login_failed";
            await JSRuntime!.InvokeAsync<object>("Page.showAlert", errorMessage);   
        }
    }
}