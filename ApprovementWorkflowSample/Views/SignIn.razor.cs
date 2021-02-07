using System.IO;
using System.Text;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApprovementWorkflowSample.Applications.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.Extensions.Configuration;
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
        public IHttpClientFactory? HttpClients { get; init; }
        [Inject]
        public IConfiguration? Configuration { get; init; }
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
            var user = await ApplicationUsers!.GetUserByEmailAsync(Email);
            if(user == null)
            {
                await HandleSigningInFailedAsync("Email or Password are not match");
                return;
            }
            var loginResult = await SignInManager!.CheckPasswordSignInAsync(user, Password, false);
            if(loginResult.Succeeded == false)
            {
                await HandleSigningInFailedAsync("Email or Password are not match");
                return;
            }
            if(loginResult.Succeeded)
            {
                var principal = await SignInManager.CreateUserPrincipalAsync(user);
                var identity = new ClaimsIdentity(principal.Claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);
                SignInManager.Context.User = principal;
                HostAuthentication!.SetAuthenticationState(
                    Task.FromResult(new AuthenticationState(principal)));

                // now the authState is updated
                var authState = await AuthenticationStateProvider!.GetAuthenticationStateAsync();
            
                Navigation!.NavigateTo("/Pages/Edit");
            }
            /*var result = await ApplicationUsers!.SignInAsync(Email, Password);
            Console.WriteLine(result);*/
            /*
            var httpClient = HttpClients.CreateClient();
            var signInValue = new SignInValue(Email, Password);
            var context = new StringContent(JsonConvert.SerializeObject(signInValue), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Path.Combine(Configuration!["BaseUrl"], "Users/SignIn"), context);
            if(response.IsSuccessStatusCode == false)
            {
                await JSRuntime!.InvokeAsync<object>("Page.showAlert","Failed access");
                return;
            }
            string resultText = await response.Content.ReadAsStringAsync();
            bool.TryParse(resultText, out var result);
            if(result)
            {
                Console.WriteLine("Navi");
                Navigation!.NavigateTo("/Pages/Edit");
                return;
            }
            AdditionalClassName = "login_failed";
            await JSRuntime!.InvokeAsync<object>("Page.showAlert","Email or Password are not match");
*/
        }
        private async Task HandleSigningInFailedAsync(string errorMessage)
        {
            AdditionalClassName = "login_failed";
            await JSRuntime!.InvokeAsync<object>("Page.showAlert", errorMessage);   
        }
    }
}